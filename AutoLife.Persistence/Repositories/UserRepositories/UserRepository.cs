using AutoLife.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Repositories.UserRepositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(DbContext context) : base(context)
    {
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Email == email && !u.IsDeleted);
    }

    public async Task<User?> GetByPhoneNumberAsync(string phoneNumber)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber && !u.IsDeleted);
    }

    public async Task<User?> GetByUserNameAsync(string userName)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.UserName == userName && !u.IsDeleted);
    }

}
