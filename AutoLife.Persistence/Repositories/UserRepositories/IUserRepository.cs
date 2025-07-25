using AutoLife.Domain.Entities;
using System.Linq.Expressions;

namespace AutoLife.Persistence.Repositories.UserRepositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByUserNameAsync(string userName);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByPhoneNumberAsync(string phoneNumber);
}
