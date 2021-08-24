using System.Collections.Generic;
using System.Security.Claims;
using RofoServer.Domain.IdentityObjects;

namespace RofoServer.Core.Logic.TokenService
{
    public interface ITokenService
    {
        string GenerateJWTToken(List<Claim> claims, string secret);
        RefreshToken GenerateRefreshToken(string ipAddress);
        List<Claim> ValidateToken(string token, string secret);
    }
}
