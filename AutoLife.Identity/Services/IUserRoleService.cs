using AutoLife.Identity.Models.IdentityEntities;
using AutoLife.Persistence.Repositories;
using System.Linq;

namespace AutoLife.Identity.Services;

public interface IUserRoleService 
{
    Task<IEnumerable<UserRole>> GetAllRolesAsync();
    Task<UserRole?> GetByIdAsync(Guid id);
    Task<UserRole> CreateAsync(string name, string? description );
    Task<bool> UpdateAsync(Guid id, string name, string? description );
    Task<bool> DeleteAsync(Guid id);
    Task<ICollection<UserRole>> GetAllByUserIdAsync(Guid id);
}