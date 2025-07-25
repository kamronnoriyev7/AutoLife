using AutoLife.Identity.Models.IdentityEntities;
using AutoLife.Identity.Repositories;
using AutoLife.Persistence.Repositories;
using AutoLife.Persistence.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Identity.Services;

public class UserRoleService : IUserRoleService
{
    private readonly IUnitOfWork<IdentityDbContext> _unitOfWork;
    private readonly IIdentityUserRoleRepository _userRoleRepository;

    public UserRoleService(IUnitOfWork<IdentityDbContext> unitOfWork, IIdentityUserRoleRepository userRoleRepository)
    {
        _unitOfWork = unitOfWork;
        _userRoleRepository = userRoleRepository;
    }

    public async Task<UserRole> CreateAsync(string name, string? description )
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 30)
        {
            throw new ArgumentException("Role name cannot be null or empty.", nameof(name));
        }

        var user =  await _userRoleRepository.GetByNameAsync(name);
        if (user != null)
        {
            throw new InvalidOperationException($"Role with name {name} already exists.");
        }

        var role = new UserRole
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            CreateDate = DateTime.UtcNow,
        };

        await _userRoleRepository.AddAsync(role);
        await _unitOfWork.SaveChangesAsync();
        return role;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var role = await _userRoleRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Role with ID {id} not found.");

        await _userRoleRepository.SoftDeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<ICollection<UserRole>> GetAllByUserIdAsync(Guid id)
    {
        var rolesQuery = await _userRoleRepository.GetAllRolesByUserId(id);
        var roles = await rolesQuery.ToListAsync();
        if (roles == null || !roles.Any())
        {
            throw new KeyNotFoundException($"No roles found for user with ID {id}.");
        }
        return roles;
    }

    public async Task<IEnumerable<UserRole>> GetAllRolesAsync()
    {
        var roles = await _userRoleRepository.GetAllAsync();
        if (roles == null || !roles.Any())
        {
            throw new KeyNotFoundException("No roles found.");
        }
        return roles;
    }

    public async Task<UserRole?> GetByIdAsync(Guid id)
    {
        return await _userRoleRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Role with ID {id} not found.");
    }

    public async Task<bool> UpdateAsync(Guid id, string name, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 30)
        {
            throw new ArgumentException("Role name cannot be null or empty.", nameof(name));
        }

        var existingRole = await _userRoleRepository.GetByIdAsync(id)
              ?? throw new KeyNotFoundException($"Role with ID {id} not found.");

        existingRole.Name = name;
        existingRole.Description = description;

        _userRoleRepository.Update(existingRole);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
