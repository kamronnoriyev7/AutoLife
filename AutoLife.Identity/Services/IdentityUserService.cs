using AutoLife.Application.DTOs.UsersDTOs;
using AutoLife.Identity.Models.AuthDTOs.Requests;
using AutoLife.Identity.Models.AuthDTOs.Responses;
using AutoLife.Identity.Models.IdentityEntities;
using AutoLife.Identity.Repositories;
using AutoLife.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Identity.Services;

public class IdentityUserService : IIdentityUserService
{
    private readonly IUnitOfWork<IdentityDbContext> _unitOfWork;    
    private readonly IIdentityUserRepository _identityUserRepository;
    private readonly IIdentityUserRoleRepository _identityRoleRepository;
    private readonly IPasswordHasherService _passwordHasherService;

    public IdentityUserService(IUnitOfWork<IdentityDbContext> unitOfWork, IIdentityUserRepository identityUserRepository, IIdentityUserRoleRepository identityRoleRepository, IPasswordHasherService passwordHasherService)
    {
        _unitOfWork = unitOfWork;
        _identityUserRepository = identityUserRepository;
        _identityRoleRepository = identityRoleRepository;
        _passwordHasherService = passwordHasherService;
    }

    public async Task<Guid> CreateAsync(RegisterRequestDto dto, Guid password)
    {
        if(password == Guid.Empty)
            throw new ArgumentException("Password cannot be empty", nameof(password));

        if (dto is null )
            throw new ArgumentNullException(nameof(dto), "RegisterRequestDto cannot be null");

        if (await _identityUserRepository.GetByUserNameAsync(dto.UserName) is not null)
            throw new Exception("UserName already exists");

        if (await _identityUserRepository.GetByEmailAsync(dto.Email) is not null)
            throw new Exception("Email already exists");

        if (await _identityUserRepository.GetByPhoneNumberAsync(dto.PhoneNumber) is not null)
            throw new Exception("Phone number already exists");

        var salt = Guid.NewGuid().ToString(); // Replace with your password hashing logic

        var passwordHash = _passwordHasherService.HashPassword(dto.Password, salt); // Replace with your password hashing logic 

        var user = new IdentityUser
        {
            Id = Guid.NewGuid(),
            UserName = dto.UserName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            PasswordSalt = salt, // Replace with your password hashing logic
            CreatedAt = DateTime.UtcNow,
            IsActive = true,
            RoleId = Guid.Parse("C5BBA2E5-09FD-4AA7-876E-F03CEB840160"), // Default role ID, should be replaced with actual role logic
            IsDeleted = false,
            IsEmailConfirmed = true, // Assuming email confirmation is required
            IsPhoneNumberConfirmed = true, // Assuming phone number confirmation is required
            PasswordHash =  passwordHash// Replace with your password hashing logic
            
        };
        await _identityUserRepository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();
        return user.Id;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id cannot be empty", nameof(id));

        var user = await _identityUserRepository.GetByIdAsync(id);
        if (user is null)
            throw new KeyNotFoundException($"User with ID {id} not found.");

        user.IsDeleted = true; // Soft delete
        _identityUserRepository.Update(user);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<UserInfoResponse>> GetAllAsync()
    {
        var users = await _identityUserRepository.GetAllAsync();
        if (users is null || !users.Any())
            return Enumerable.Empty<UserInfoResponse>();
        return users.Select(user => new UserInfoResponse
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            CreatedAt = user.CreatedAt,
            
        });
    }

    public async Task<UserInfoResponse?> GetByIdAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id cannot be empty", nameof(id));

        var user = await _identityUserRepository.GetByIdAsync(id);
        if (user is null)
            return null;

        return new UserInfoResponse
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            CreatedAt = user.CreatedAt,
        };
    }

    public async Task<Guid> GetIdentityIdByEmailAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be null or empty", nameof(email));

        var user = await _identityUserRepository.GetByEmailAsync(email);

        if (user is null)
            throw new KeyNotFoundException($"User with email {email} not found.");

        return user.Id;
    }

    public async Task<Guid> GetIdentityIdByPhoneNumberAsync(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("PhoneNumber cannot be null or empty", nameof(phoneNumber));

        var user = await _identityUserRepository.GetByPhoneNumberAsync(phoneNumber);

        if (user is null)
            throw new KeyNotFoundException($"User with phone number {phoneNumber} not found.");
        return user.Id;
    }

    public async Task<Guid> GetUserIdByIdentityIdAsync(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
            throw new ArgumentException("UserName cannot be null or empty", nameof(userName));

        var user = await _identityUserRepository.GetByUserNameAsync(userName);

        if (user is null)
            throw new KeyNotFoundException($"User with UserName {userName} not found.");

        return user.UserId;
    }

    public async Task<bool> IsEmailExistsAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be null or empty", nameof(email));

        var user = await _identityUserRepository.GetByEmailAsync(email);
        return user != null;
    }

    public async Task<bool> IsUserNameExistsAsync(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
            throw new ArgumentException("UserName cannot be null or empty", nameof(userName));

        userName = userName.Trim();

        var user = await _identityUserRepository.GetByUserNameAsync(userName);
        return user != null;
    } 

    public async Task Remove(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id cannot be empty", nameof(id));

        var user = await _identityUserRepository.GetByIdAsync(id);

        if (user is null)
            throw new KeyNotFoundException($"User with ID {id} not found.");

        user.IsDeleted = true; // Soft delete
        _identityUserRepository.Update(user);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(Guid id, IdentityUserUpdateDto dto)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id cannot be empty", nameof(id));

        if (dto is null)
            throw new ArgumentNullException(nameof(dto), "UserInfoResponse cannot be null");

        var user = await _identityUserRepository.GetByIdAsync(id);
        if (user is null)
            throw new KeyNotFoundException($"User with ID {id} not found.");

        user.UserName = dto.UserName ?? user.UserName;
        user.Email = dto.Email ?? user.Email;
        user.PhoneNumber = dto.PhoneNumber ?? user.PhoneNumber;

        _identityUserRepository.Update(user);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
