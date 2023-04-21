
using Data.Models;
using System.Security.Claims;

namespace Infrastructure
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        RefreshTokenModel GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
