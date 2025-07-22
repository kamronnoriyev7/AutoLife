using AutoLife.Identity.Models.IdentityEntities;
using AutoLife.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Identity.Repositories;

public interface IUserRoleRepository : IGenericRepository<UserRole>
{
    Task<UserRole?> GetByNameAsync(string roleName);
}
