using AutoLife.Domain.Entities;
using AutoLife.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Infrastructure.Services.UserServices;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(long id);
    Task<User?> GetByUsernameAsync(string username);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(long id);

    Task<bool> IsUsernameTakenAsync(string username);
    Task<IEnumerable<User>> SearchByNameAsync(string name);
    Task<IEnumerable<User>> GetUsersByRoleAsync(UserRole userRole);
    Task<User?> AuthenticateAsync(string username, string password);
    Task ChangePasswordAsync(long userId, string currentPassword, string newPassword);
}

