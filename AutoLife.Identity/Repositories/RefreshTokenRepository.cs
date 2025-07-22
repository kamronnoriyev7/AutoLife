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

public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(IdentityDbContext context) : base(context) { }

    public async Task<bool> ExistsAsync(Expression<Func<RefreshToken, bool>> predicate)
    {
        return await _dbSet.AnyAsync(predicate);
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token)
        => await _dbSet.FirstOrDefaultAsync(r => r.Token == token && !r.IsDeleted);

    public async Task<IEnumerable<RefreshToken>> GetByUserIdAsync(Guid identityUserId)
    {
        return await _dbSet
            .Where(r => r.IdentityUserId == identityUserId && !r.IsRevoked && !r.IsDeleted)
            .ToListAsync();
    }

    public async Task<IEnumerable<RefreshToken>> GetUserTokensAsync(Guid identityUserId)
        => await _dbSet.Where(r => r.IdentityUserId == identityUserId && !r.IsRevoked && !r.IsDeleted).ToListAsync();

    public async Task RevokeTokenAsync(string token)
    {
        var entity = await _dbSet.FirstOrDefaultAsync(r => r.Token == token);
        if (entity is not null)
        {
            entity.IsRevoked = true;
            _dbSet.Update(entity);
        }
    }
}
