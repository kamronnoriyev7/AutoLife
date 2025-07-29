using Amazon.SimpleSystemsManagement.Model;
using AutoLife.Identity.Models.AuthDTOs.Requests;
using AutoLife.Identity.Models.AuthDTOs.Responses;
using AutoLife.Identity.Repositories;
using AutoLife.Persistence.UnitOfWork;
using SendGrid.Helpers.Errors.Model;
using IdentityUser = AutoLife.Identity.Models.IdentityEntities.IdentityUser;

namespace AutoLife.Identity.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork<IdentityDbContext> _unitOfWork;
    private readonly IIdentityUserRepository _userRepo;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly IPasswordHasherService _passwordHasher;
    private readonly ITokenService _tokenService;
    private readonly IVerificationCodeRepository _verificationCodeRepository;

    public AuthService(IUnitOfWork<IdentityDbContext> unitOfWork, IIdentityUserRepository userRepo, IRefreshTokenService refreshTokenService, IPasswordHasherService passwordHasher, ITokenService tokenService, IVerificationCodeRepository verificationCodeRepository)
    {
        _unitOfWork = unitOfWork;
        _userRepo = userRepo;
        _refreshTokenService = refreshTokenService;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
        _verificationCodeRepository = verificationCodeRepository;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequestDto dto)
    {
        if (await _userRepo.GetByUserNameAsync(dto.UserName) is not null)
            throw new AlreadyExistsException("UserName already exists");

        var existingUserByEmail = await _userRepo.GetByEmailAsync(dto.Email);
        if (existingUserByEmail is not null)
            throw new AlreadyExistsException("Email already exists");

        var existingUserByPhone = await _userRepo.GetByPhoneNumberAsync(dto.PhoneNumber);
        if (existingUserByPhone is not null)
            throw new AlreadyExistsException("Phone number already exists");

        var isEmailVerified = await _verificationCodeRepository.GetCodeAsync(dto.Email)
            ?? throw new NotFoundException("Email is  not found");

        if (isEmailVerified.IsUsed)
            throw new AlreadyExistsException("Email is already in use");

        var salt = _passwordHasher.GenerateSalt();
        var passwordHash = _passwordHasher.HashPassword(dto.Password, salt);

        var user = new IdentityUser
        {
            Id = Guid.NewGuid(),
            UserName = dto.UserName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            PasswordSalt = salt,
            PasswordHash = passwordHash,
            CreatedAt = DateTime.UtcNow,
            IsActive = true,
            IsEmailConfirmed = true, // Assuming email is verified during registration
            RoleId = Guid.Parse("C5BBA2E5-09FD-4AA7-876E-F03CEB840160"), // Default role ID, should be replaced with actual role logic
            UserId = Guid.NewGuid() // Assuming a new UserId is generated for the user
        };

        isEmailVerified.IsVerified = true; // Mark email as verified
        _verificationCodeRepository.Update(isEmailVerified);

        await _userRepo.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        var refreshToken = await _refreshTokenService.CreateTokenAsync(user.Id);
        var accessToken = _tokenService.GenerateAccessToken(user);

        return new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token,
            ExpireAt = refreshToken.ExpiresAt
        };
    }

    public async Task<AuthResponse> LoginAsync(LoginRequestDto dto)
    {
        var user = await _userRepo.GetByUserNameAsync(dto.UserName);
        if (user is null || !_passwordHasher.Verify(dto.Password, user.PasswordHash, user.PasswordSalt))
            throw new UnauthorizedException("Invalid credentials");

        var refreshToken = await _refreshTokenService.CreateTokenAsync(user.Id);
        var accessToken = _tokenService.GenerateAccessToken(user);

        return new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token,
            ExpireAt = refreshToken.ExpiresAt
        };
    }

    public async Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequestDto dto)
    {
        var token = await _refreshTokenService.GetTokenAsync(dto.RefreshToken);
        if (token is null || token.IsRevoked || token.ExpiresAt < DateTime.UtcNow)
            throw new UnauthorizedException("Invalid refresh token");

        var user = await _userRepo.GetByIdAsync(token.IdentityUserId);
        if (user is null)
            throw new NotFoundException("User not found");

        await _refreshTokenService.RevokeAsync(token.Token);
        var newRefreshToken = await _refreshTokenService.CreateTokenAsync(user.Id);
        var newAccessToken = _tokenService.GenerateAccessToken(user);

        return new AuthResponse
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken.Token,
            ExpireAt = newRefreshToken.ExpiresAt
        };
    }

    public async Task ChangePasswordAsync(Guid identityUserId, ChangePasswordRequestDto dto)
    {
        var user = await _userRepo.GetByIdAsync(identityUserId);
        if (user is null)
            throw new NotFoundException("User not found");

        if (!_passwordHasher.Verify(dto.OldPassword, user.PasswordHash, user.PasswordSalt))
            throw new UnauthorizedException("Old password is incorrect");

        user.PasswordSalt = _passwordHasher.GenerateSalt();
        user.PasswordHash = _passwordHasher.HashPassword(dto.NewPassword, user.PasswordSalt);

        _userRepo.Update(user);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task LogoutAsync(string refreshToken)
    {
        var isValid = await _refreshTokenService.IsValidAsync(refreshToken);
        if (!isValid)
        {
            throw new UnauthorizedException("Invalid refresh token");
        }
        await _refreshTokenService.RevokeAsync(refreshToken);
    }
}


