using AutoLife.Domain.Entities;
using AutoLife.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Persistence.Repositories.Users;

public interface IUserRepository :IGenericRepository<User>
{
    Task<User?> GetByIdentityUserId(long identityUserId);
    Task<bool> IsExist(long userId);
}

