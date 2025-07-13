using AutoLife.Domain.Entities;
using AutoLife.Domain.Enums;
using AutoLife.Persistence.DataBaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Repositories.Users;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByIdentityUserId(long identityUserId)
    {
        var users = await FindAsync(u => u.Id == identityUserId);
        return users.FirstOrDefault();
    }

    public async Task<bool> IsExist(long userId)
    {
        var users = await FindAsync(u => u.Id == userId);
        return users.Any();
    }
    public override async Task<User?> GetByIdAsync(
     long id,
     string includeProperties = "",
     bool includeDeleted = false,
     bool asNoTracking = false)
    {
        IQueryable<User> query = _dbSet;

        // 1. AsNoTracking qo‘llash (agar kerak bo‘lsa)
        if (asNoTracking)
            query = query.AsNoTracking();

        // 2. Include navigatsiya propertilar
        if (!string.IsNullOrWhiteSpace(includeProperties))
        {
            foreach (var includeProp in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp.Trim());
            }
        }

        // 3. Soft delete'ni hisobga olish
        if (!includeDeleted)
            query = query.Where(u => !u.IsDeleted);

        // 4. Id bo‘yicha filter
        return await query.FirstOrDefaultAsync(u => u.Id == id);
    }


    public Task<User?> GetByIdAsync(Guid id, string includeProperties = "", bool includeDeleted = false, bool asNoTracking = false)
    {
        throw new NotImplementedException();
    }
}
