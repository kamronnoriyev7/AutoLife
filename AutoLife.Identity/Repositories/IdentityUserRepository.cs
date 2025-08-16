using AutoLife.Identity.Models.IdentityEntities;
using AutoLife.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Identity.Repositories;

public class IdentityUserRepository(IdentityDbContext context) : GenericRepository<IdentityUser, IdentityDbContext>(context), IIdentityUserRepository
{
    public async Task<IdentityUser?> GetByUserNameAsync(string userName)
        => await _dbSet.FirstOrDefaultAsync(u => u.UserName == userName);

    public async Task<IdentityUser?> GetByEmailAsync(string email)
        => await _dbSet.FirstOrDefaultAsync(u => u.Email == email);

    public async Task<IdentityUser?> GetByPhoneNumberAsync(string phoneNumber)
        => await _dbSet.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);

    public async Task<bool> ExistsAsync(Expression<Func<IdentityUser, bool>> predicate)
        => await _dbSet.AnyAsync(predicate);
}