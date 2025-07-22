using AutoLife.Identity.Models.IdentityEntities;
using AutoLife.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Identity.Repositories;

public class UserRoleRepository : GenericRepository<UserRole>, IUserRoleRepository
{
    public UserRoleRepository(IdentityDbContext context) : base(context)
    {
    }

    public async Task<UserRole?> GetByNameAsync(string roleName)
        => await _dbSet.FirstOrDefaultAsync(r => r.Name == roleName);
}