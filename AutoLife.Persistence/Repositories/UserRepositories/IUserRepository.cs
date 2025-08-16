using AutoLife.Domain.Entities;
using AutoLife.Persistence.DataBaseContext;
using System.Linq.Expressions;

namespace AutoLife.Persistence.Repositories.UserRepositories;

public interface IUserRepository : IGenericRepository<User, AppDbContext>
{
    Task<User?> GetByUserNameAsync(string userName);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByPhoneNumberAsync(string phoneNumber);
}
