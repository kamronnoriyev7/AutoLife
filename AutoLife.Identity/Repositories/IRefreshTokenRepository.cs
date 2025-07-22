using AutoLife.Identity.Models.IdentityEntities;
using AutoLife.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Identity.Repositories;

public interface IRefreshTokenRepository : IGenericRepository<RefreshToken>
{
    Task<RefreshToken?> GetByTokenAsync(string token);
    Task<IEnumerable<RefreshToken>> GetByUserIdAsync(Guid identityUserId);
    Task RevokeTokenAsync(string token);
    Task<bool> ExistsAsync(Expression<Func<RefreshToken, bool>> predicate);
}