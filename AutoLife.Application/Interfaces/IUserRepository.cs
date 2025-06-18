using AutoLife.Domain.Entities;
using AutoLife.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Application.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByUserNameAsync(string username);
    Task<User?> GetByEmailAsync(string email);
}

