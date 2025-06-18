using AutoLife.Application.DTOs.AuthDTOs;
using AutoLife.Application.DTOs.UsersDTOs;
using AutoLife.Domain.Entities;
using AutoLife.Infrastructure.Services.TokenServices;
using AutoLife.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Infrastructure.Services.AuthServices;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;

    public AuthService(IUnitOfWork unitOfWork, ITokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
    }

    public async Task<UserResponseDto> SignUpAsync(RegisterRequestDto signUpDto)
    {
        var userRepo = _unitOfWork.Repository<User>();

        var existingUser = (await userRepo.GetAllAsync())
            .FirstOrDefault(u => u.UserName == signUpDto.UserName);

        if (existingUser != null)
            throw new Exception("Bu username allaqachon mavjud");

        var salt = BCrypt.Net.BCrypt.GenerateSalt(12);

        var user = new User
        {
            FirstName = signUpDto.FirstName,
            LastName = signUpDto.LastName,
            UserName = signUpDto.UserName,
            Email = signUpDto.Email,
            PasswordSalt = salt,
            Role = signUpDto.Role, // Assuming Role is part of RegisterRequestDto
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(signUpDto.Password)
        };

        await userRepo.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return new UserResponseDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            Email = user.Email
        };
    }

    public async Task<UserResponseDto> LoginAsync(LoginRequestDto loginDto)
    {
        var userRepo = _unitOfWork.Repository<User>();

        var user = (await userRepo.GetAllAsync())
            .FirstOrDefault(u => u.UserName == loginDto.UserName);

        if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            throw new Exception("Username yoki parol noto‘g‘ri");

        var token = _tokenService.GenerateToken(user.UserName);

        return new UserResponseDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            Email = user.Email,
            Token = token // Assuming UserResponseDto has a Token property
        };
    }

    public async Task LogoutAsync(string username)
    {
        // Agar refresh token saqlanayotgan bo‘lsa, shu yerda uni o‘chirish mumkin
        await Task.CompletedTask;
    }
}

