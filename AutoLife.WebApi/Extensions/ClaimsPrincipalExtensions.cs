using System.Security.Claims;

namespace AutoLife.Api.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var claim = user.FindFirst(ClaimTypes.NameIdentifier);
        if (claim == null) throw new UnauthorizedAccessException("User ID not found");
        return Guid.Parse(claim.Value);
    }
}
