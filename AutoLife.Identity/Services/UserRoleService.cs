using AutoLife.Identity.Models.IdentityEntities;
using AutoLife.Identity.Repositories;
using AutoLife.Persistence.Repositories;
using AutoLife.Persistence.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Identity.Services;

public class UserRoleService : IUserRoleService
{
    private readonly IUnitOfWork<IdentityDbContext> _unitOfWork;
    private readonly IGenericRepository<UserRole> _roleRepo;

    public UserRoleService(IUnitOfWork<IdentityDbContext> unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _roleRepo = unitOfWork.Repository<UserRole>();
    }

    public async Task<IEnumerable<UserRole>> GetAllRolesAsync()
    {
        return await _roleRepo.GetAllAsync();
    }

    public async Task<UserRole?> GetByIdAsync(Guid id)
    {
        return await _roleRepo.GetByIdAsync(id);
    }

    public async Task<UserRole> CreateAsync(string name, string? description = null)
    {
        var role = new UserRole
        {
            Name = name,
            Description = description
        };

        var existing = await _roleRepo.FindAsync(r => r.Name == name);
        if (existing != null)
            throw new Exception("Role already exists");

        await _roleRepo.AddAsync(role);
        await _unitOfWork.SaveChangesAsync();
        return role;
    }

    public async Task<bool> UpdateAsync(Guid id, string name, string? description = null)
    {
        var existing = await _roleRepo.GetByIdAsync(id);
        if (existing is null) return false;

        existing.Name = name;
        existing.Description = description;

        _roleRepo.Update(existing);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var existing = await _roleRepo.GetByIdAsync(id);
        if (existing is null) return false;

        _roleRepo.Remove(existing);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
