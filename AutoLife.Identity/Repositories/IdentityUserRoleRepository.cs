using AutoLife.Identity.Models.IdentityEntities;
using AutoLife.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Identity.Repositories;

public class IdentityUserRoleRepository : GenericRepository<UserRole>, IIdentityUserRoleRepository
{
    public IdentityUserRoleRepository(IdentityDbContext context) : base(context)
    {
    }

    public async Task<IQueryable<UserRole>> GetAllRolesByUserId(Guid userId)
    {
      var query = _dbSet.AsQueryable()
          .Where(r => r.Users.Any(u => u.UserId == userId && !u.IsDeleted));

        return await Task.FromResult(query);
    }

    public async Task<UserRole?> GetByNameAsync(string roleName)
    {
        return await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Name == roleName && !r.IsDeleted);
    }
       

}