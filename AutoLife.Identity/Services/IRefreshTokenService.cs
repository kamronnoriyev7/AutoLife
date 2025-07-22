using AutoLife.Identity.Models.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLife.Identity.Services;

public interface IRefreshTokenService
{
    Task<RefreshToken> CreateTokenAsync(Guid identityUserId);
    Task<bool> IsValidAsync(string token);
    Task RevokeAsync(string token);
    Task<RefreshToken?> GetTokenAsync(string refreshToken);

}
