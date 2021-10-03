using System.Collections.Generic;
using System.Security.Claims;
using RofoServer.Domain.IdentityObjects;

namespace RofoServer.Core.Utils.TokenService
{
    public interface IJwtServices
    {
        string GenerateJwtToken(List<UserClaim> claims);
        RefreshToken GenerateRefreshToken();
        List<Claim> ValidateJwtToken(string token);
        void RotateRefreshToken(List<RefreshToken> userTokens);
        void RevokeDescendantRefreshTokens(RefreshToken refreshToken, List<RefreshToken> userTokens, string reason);
        void RevokeRefreshToken(RefreshToken token, string reason = null, string replacedByToken = null);
    }
}
