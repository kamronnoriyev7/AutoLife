using AutoLife.Application.DTOs.UsersDTOs;
using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;
using AutoLife.Persistence.Repositories;
using AutoLife.Persistence.UnitOfWork;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
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
    private readonly IWebHostEnvironment _env;

    public UserService(IUnitOfWork<AppDbContext> unitOfWork, IGenericRepository<User> userRepository, IWebHostEnvironment env)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _env = env;
    }

    public async Task<UserResponseDto> CreateUserAsync(UserCreateDto user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        if (await UserExistsByEmailAsync(user.Email))
            throw new InvalidOperationException($"User with email {user.Email} already exists.");

        if (await UserExistsByPhoneNumberAsync(user.PhoneNumber))
            throw new InvalidOperationException($"User with phone number {user.PhoneNumber} already exists.");

        if (await UserExistsByUserNameAsync(user.UserName))
            throw new InvalidOperationException($"User with user name {user.UserName} already exists.");

        var newUser = new User
        {
            Id = Guid.NewGuid(),
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            PhoneNumber = user.PhoneNumber,
            Email = user.Email,
            DateOfBirth = user.DateOfBirth,
            IdentityUserId = user.IdentityUserId,
            IsActive = true,
            Images = new List<Image>(),
            CreateDate = DateTime.UtcNow
        };

        if (user.ProfileImages is not null)
        {
            foreach (var file in user.ProfileImages)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(stream);

                newUser.Images.Add(new Image
                {
                    Id = Guid.NewGuid(),
                    UserId = newUser.Id,
                    ImageUrl = $"/uploads/{fileName}" // ← bu yerda "Path" o‘rniga sizda "FilePath" bo‘lishi kerak
                });
            }
        }

        await _userRepository.AddAsync(newUser);
        await _unitOfWork.SaveChangesAsync();

        var _mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserResponseDto>()).CreateMapper();

        var response = _mapper.Map<UserResponseDto>(newUser);
        response.ProfileImages = newUser.Images
            .Where(i => !string.IsNullOrWhiteSpace(i.ImageUrl))
            .Select(i => i.ImageUrl!)
            .ToList();

        return response;
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

    public async Task<UserResponseDto> UpdateUserAsync(UserUpdateDto user)
    {
        var existingUser = await _userRepository.GetByIdAsync(user.UserId);

        if (existingUser == null)
            throw new KeyNotFoundException($"User with ID {user.UserId} not found.");

        // Email, Phone, UserName ni yangilashdan oldin mavjudligini tekshiramiz
        if (!string.Equals(existingUser.Email, user.Email, StringComparison.OrdinalIgnoreCase) &&
            await UserExistsByEmailAsync(user.Email))
            throw new InvalidOperationException($"User with email {user.Email} already exists.");

        if (!string.Equals(existingUser.PhoneNumber, user.PhoneNumber, StringComparison.OrdinalIgnoreCase) &&
            await UserExistsByPhoneNumberAsync(user.PhoneNumber))
            throw new InvalidOperationException($"User with phone number {user.PhoneNumber} already exists.");

        if (!string.Equals(existingUser.UserName, user.UserName, StringComparison.OrdinalIgnoreCase) &&
            await UserExistsByUserNameAsync(user.UserName))
            throw new InvalidOperationException($"User with username {user.UserName} already exists.");

        // Ma'lumotlarni yangilaymiz
        existingUser.FirstName = user.FirstName;
        existingUser.LastName = user.LastName;
        existingUser.UserName = user.UserName;
        existingUser.PhoneNumber = user.PhoneNumber;
        existingUser.Email = user.Email;
        existingUser.DateOfBirth = user.DateOfBirth;
        existingUser.UpdateDate = DateTime.UtcNow;

        // Agar rasm(lar) yuborilgan bo‘lsa, ularni serverga saqlaymiz va yangi Image lar qo‘shamiz
        if (user.ProfilePictureUrl is not null)
        {
            foreach (var file in user.ProfilePictureUrl)
            {
                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(stream);

                if (existingUser.Images == null)
                {
                    existingUser.Images = new List<Image>();
                }

                existingUser.Images.Add(new Image
                {
                    Id = Guid.NewGuid(),
                    UserId = existingUser.Id,
                    ImageUrl = $"/uploads/{fileName}"
                });
            }
        }

        _userRepository.Update(existingUser);
        await _unitOfWork.SaveChangesAsync();

        // Map qilish
        var _mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserResponseDto>()).CreateMapper();
        var response = _mapper.Map<UserResponseDto>(existingUser);
        response.ProfileImages = existingUser.Images is not null
             ? existingUser.Images
                 .Where(i => !string.IsNullOrWhiteSpace(i.ImageUrl))
                 .Select(i => i.ImageUrl!)
                 .ToList()
             : new List<string>();

        return response;
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
