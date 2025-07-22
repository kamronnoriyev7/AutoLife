using AutoLife.Identity.Models.IdentityEntities;
using AutoLife.Persistence.Repositories;

namespace AutoLife.Identity.Services;

public interface IUserRoleService 
{
    Task<IEnumerable<UserRole>> GetAllRolesAsync();
    Task<UserRole?> GetByIdAsync(Guid id);
    Task<UserRole> CreateAsync(string name, string? description = null);
    Task<bool> UpdateAsync(Guid id, string name, string? description = null);
    Task<bool> DeleteAsync(Guid id);
}