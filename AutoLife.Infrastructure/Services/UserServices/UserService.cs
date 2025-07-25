using AutoLife.Application.DTOs.UsersDTOs;
using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;
using AutoLife.Persistence.Repositories;
using AutoLife.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Infrastructure.Services.UserServices;

public class UserService : IUserService
{
    private readonly IUnitOfWork<AppDbContext> _unitOfWork;
    private readonly IGenericRepository<User> _userRepository;

    public UserService(IUnitOfWork<AppDbContext> unitOfWork, IGenericRepository<User> userRepository)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    public async Task<User> CreateUserAsync(UserCreateDto user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        var existingUser = await _userRepository.GetByIdAsync(user.Id);
        if (existingUser != null)
        {
            throw new InvalidOperationException($"User with ID {user.Id} already exists.");
        }

        var newUser = new User
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email,
            DateOfBirth = user.DateOfBirth,
            IsActive = true, 
            IdentityUserId = user.IdentityUserId 
        };
        await _userRepository.AddAsync(newUser);
        await _unitOfWork.SaveChangesAsync();
        return newUser;
    }

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        if (id == Guid.Empty) throw new ArgumentException("Invalid user ID.", nameof(id));

        var  existingUser = await _userRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"User with ID {id} not found.");

        await _userRepository.SoftDeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
        return true;

    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        if (users == null || !users.Any())
        {
            throw new KeyNotFoundException("No users found.");
        }
        return users;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be null or empty.", nameof(email));

        var user = await _userRepository.FindAsync(u => u.Email == email);

        return user.FirstOrDefault() 
            ?? throw new KeyNotFoundException($"User with email {email} not found.");
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await _userRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"User with ID {id} not found.");
    }

    public async Task<User?> GetUserByPhoneNumberAsync(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Phone number cannot be null or empty.", nameof(phoneNumber));

        var user = await _userRepository.FindAsync(u => u.PhoneNumber == phoneNumber);

        return user.FirstOrDefault() 
            ?? throw new KeyNotFoundException($"User with phone number {phoneNumber} not found.");
    }

    public async Task<User?> GetUserByUserNameAsync(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
            throw new ArgumentException("User name cannot be null or empty.", nameof(userName));

        var user = await _userRepository.FindAsync(u => u.UserName == userName);

        return user.FirstOrDefault() 
            ?? throw new KeyNotFoundException($"User with user name {userName} not found.");
    }

    public async Task<bool> UpdateUserAsync(UserCreateDto user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        var existingUser = await _userRepository.GetByIdAsync(user.Id)
            ?? throw new KeyNotFoundException($"User with ID {user.Id} not found.");

        existingUser.FirstName = user.FirstName;
        existingUser.LastName = user.LastName;
        existingUser.UserName = user.UserName;
        existingUser.PhoneNumber = user.PhoneNumber;
        existingUser.Email = user.Email;
        existingUser.DateOfBirth = user.DateOfBirth;
        existingUser.UpdateDate = DateTime.UtcNow;

        _userRepository.Update(existingUser);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UserExistsAsync(Guid id)
    {
        if (id == Guid.Empty) throw new ArgumentException("Invalid user ID.", nameof(id));
        var user = await _userRepository.GetByIdAsync(id);
        return user != null;
    }

    public async Task<bool> UserExistsByEmailAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be null or empty.", nameof(email));

        var user = await _userRepository.FindAsync(u => u.Email == email);
        return user.Any();
    }

    public async Task<bool> UserExistsByPhoneNumberAsync(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Phone number cannot be null or empty.", nameof(phoneNumber));

        var user = await _userRepository.FindAsync(u => u.PhoneNumber == phoneNumber);
        return user.Any();
    }

    public async Task<bool> UserExistsByUserNameAsync(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
            throw new ArgumentException("User name cannot be null or empty.", nameof(userName));

        var user = await _userRepository.FindAsync(u => u.UserName == userName);
        return user.Any();
    }
}
