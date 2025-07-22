using AutoLife.Identity.Models.IdentityEntities;
using AutoLife.Identity.Repositories;
using AutoLife.Persistence.DataBaseContext;
using AutoLife.Persistence.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Identity.Services;

public class RefreshTokenService : IRefreshTokenService 
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUnitOfWork<IdentityDbContext> _unitOfWork;


    public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository, IUnitOfWork<IdentityDbContext> unitOfWork)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<RefreshToken> CreateTokenAsync(Guid identityUserId)
    {
        var token = new RefreshToken
        {
            IdentityUserId = identityUserId,
            Token = Guid.NewGuid().ToString("N"),
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            IsRevoked = false,
            CreatedAt = DateTime.UtcNow
        };

        await _refreshTokenRepository.AddAsync(token);
        await _unitOfWork.SaveChangesAsync();

        return token;
    }

    public async Task<bool> IsValidAsync(string token)
    {
        var refreshToken = await _refreshTokenRepository.GetByTokenAsync(token);
        return refreshToken is not null && !refreshToken.IsRevoked && refreshToken.ExpiresAt > DateTime.UtcNow;
    }

    public async Task RevokeAsync(string token)
    {
        var refreshToken = await _refreshTokenRepository.GetByTokenAsync(token);
        if (refreshToken is null) return;

        refreshToken.IsRevoked = true;
        _refreshTokenRepository.Update(refreshToken);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<RefreshToken?> GetTokenAsync(string refreshToken)
    {
        return await _refreshTokenRepository.GetByTokenAsync(refreshToken);
    }
}