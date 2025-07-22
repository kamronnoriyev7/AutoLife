using AutoLife.Identity.Models.IdentityEntities;
using AutoLife.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Identity.Repositories;
public interface IIdentityUserRepository : IGenericRepository<IdentityUser>
{
    Task<IdentityUser?> GetByUserNameAsync(string userName);
    Task<IdentityUser?> GetByEmailAsync(string email);
    Task<IdentityUser?> GetByPhoneNumberAsync(string phoneNumber);
    Task<bool> ExistsAsync(Expression<Func<IdentityUser, bool>> predicate);
}