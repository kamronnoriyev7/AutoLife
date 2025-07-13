using AutoLife.Application.DTOs.UsersDTOs;
using AutoLife.Domain.Entities;
using AutoLife.Domain.Enums;
using AutoLife.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Infrastructure.Services.UserServices;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;


    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _unitOfWork.Repository<User>().GetAllAsync()
            ?? throw new Exception("No users found");
    }

    public async Task<User?> GetByIdAsync(long id)
    {
        return await _unitOfWork.Repository<User>().GetByIdAsync(id)
            ?? throw new Exception("User not found");
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        var users = await _unitOfWork.Repository<User>().FindAsync(u => u.UserName == username)
            ?? throw new Exception("User not found with the specified username");

        return users.FirstOrDefault()
            ?? throw new Exception("User not found with the specified username");
    }
    public async Task AddAsync(User user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user), "User cannot be null.");

        if (string.IsNullOrWhiteSpace(user.UserName))
            throw new ArgumentException("Username cannot be null or empty.", nameof(user.UserName));

        if (await IsUsernameTakenAsync(user.UserName))
            throw new Exception("Username is already taken.");

        await _unitOfWork.Repository<User>().AddAsync(user);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        var existingUser = await _unitOfWork.Repository<User>().GetByIdAsync(user.Id)
                     ?? throw new Exception("User not found");
        existingUser.FirstName = user.FirstName;
        existingUser.LastName = user.LastName;
        existingUser.Email = user.Email;
        existingUser.PhoneNumber = user.PhoneNumber;
        existingUser.UserName = user.UserName;
        existingUser.PasswordHash = user.PasswordHash;

        _unitOfWork.Repository<User>().Update(existingUser);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var user = await _unitOfWork.Repository<User>().GetByIdAsync(id);
        if (user == null)
            throw new Exception("User not found");

        _unitOfWork.Repository<User>().Remove(user);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> IsUsernameTakenAsync(string username)
    {
        var users = await _unitOfWork.Repository<User>().FindAsync(u => u.UserName == username);
        return users.Any();
    }

    public async Task<IEnumerable<User>> SearchByNameAsync(string name)
    {
        return await _unitOfWork.Repository<User>().FindAsync(
            u => u.FirstName.Contains(name) || u.LastName.Contains(name)
        ) 
            ?? throw new Exception("No users found with the specified name");
    }

    public async Task<IEnumerable<User>> GetUsersByRoleAsync(UserRole role)
    {
        return await _unitOfWork.Repository<User>().FindAsync(
            u => u.Role == role
        )
            ?? throw new Exception("No users found with the specified role");
    }

    public async Task<User?> AuthenticateAsync(string username, string password)
    {
        var users = await _unitOfWork.Repository<User>().FindAsync(
            u => u.UserName == username && u.PasswordHash == password
        ) 
            ?? throw new Exception("Invalid username or password");

        return users.FirstOrDefault() 
            ?? throw new Exception("Invalid username or password");
    }

    public Task ChangePasswordAsync(long userId, string currentPassword, string newPassword)
    {
        throw new NotImplementedException();
    }

    //public async Task ChangePasswordAsync(long userId, string currentPassword, string newPassword)
    //{
    //    var user = await _unitOfWork.Repository<User>().GetByIdAsync(userId);
    //    if (user == null)
    //        throw new Exception("User not found");

    //    if (user.Password != currentPassword)
    //        throw new Exception("Current password is incorrect");

    //    user.Password = newPassword;
    //    _unitOfWork.Repository<User>().Update(user);
    //    await _unitOfWork.SaveChangesAsync();
    //}
}

