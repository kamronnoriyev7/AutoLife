using AutoLife.Application.DTOs.UsersDTOs;
using AutoLife.Domain.Entities;

namespace AutoLife.Infrastructure.Services.UserServices;

public interface IUserService 
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User?> GetUserByIdAsync(Guid id);
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByPhoneNumberAsync(string phoneNumber);
    Task<User?> GetUserByUserNameAsync(string userName);
    Task<bool> UserExistsAsync(Guid id);
    Task<bool> UserExistsByEmailAsync(string email);
    Task<bool> UserExistsByPhoneNumberAsync(string phoneNumber);
    Task<bool> UserExistsByUserNameAsync(string userName);
    Task<UserResponseDto> CreateUserAsync(UserCreateDto dto);
    Task<UserResponseDto> UpdateUserAsync(UserUpdateDto user);
    Task<bool> DeleteUserAsync(Guid id);
}