using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RofoServer.Domain.IdentityObjects;

namespace RofoServer.Core.Utils.TokenService
{
    public class JwtServices : IJwtServices
    {
        private readonly IConfiguration _configuration;

        public JwtServices(IConfiguration config) {
            _configuration = config;
        }

        public string GenerateJwtToken(List<UserClaim> claims) {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(
                new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(getClaims(claims)),
                    Expires = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["AppSettings:JWTExpiryMinutes"])),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(
                            Encoding.ASCII.GetBytes(_configuration["Rofos:ApiKey"])),
                        SecurityAlgorithms.HmacSha512Signature)
                });
            return tokenHandler.WriteToken(token);
        }

        public RefreshToken GenerateRefreshToken() {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[64];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return new RefreshToken {
                Token = Convert.ToBase64String(randomBytes),
                Expires = DateTime.UtcNow.AddDays(int.Parse(_configuration["AppSettings:RefreshTokenExpiryDays"])),
                Created = DateTime.UtcNow,
                CreatedByIp = _configuration["AppSettings:ServerIPAddress"]
            };
        }

        public List<Claim> ValidateJwtToken(string token)
        {
            if (token == null)
                return null;

            var myClaims = new List<Claim>();
            foreach (var c in validateToken(token).Claims)
                myClaims.Add(c);

            return myClaims;
        }

        public void RevokeDescendantRefreshTokens(RefreshToken refreshToken, List<RefreshToken> userTokens, string reason) {
            if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken)) {
                var childToken = userTokens.SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);
                if (childToken.IsActive)
                    RevokeRefreshToken(childToken, reason);
                else
                    RevokeDescendantRefreshTokens(childToken, userTokens, reason);
            }
        }

        public void RevokeRefreshToken(RefreshToken token, string reason = null, string replacedByToken = null) {
            token.Revoked = DateTime.UtcNow;
            token.RevokedByIp = _configuration.GetSection("ServerIPAddress").Value;
            token.ReasonRevoked = reason;
            token.ReplacedByToken = replacedByToken;
        }

        public void RotateRefreshToken(List<RefreshToken> userTokens) {
            userTokens.Add(GenerateRefreshToken());
            if (userTokens.Any(x => x.IsActive && x.Token != userTokens.Last().Token))
                RevokeRefreshToken(userTokens.SingleOrDefault(x => x.IsActive && x.Token != userTokens.Last().Token), "Replaced by new token", userTokens.Last().Token);
            removeOldRefreshTokens(userTokens);
        }

        private JwtSecurityToken validateToken(string token) {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Rofos:ApiKey"]);
            tokenHandler.ValidateToken(token, new TokenValidationParameters {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            return validatedToken as JwtSecurityToken;
        }

        private void removeOldRefreshTokens(List<RefreshToken> userTokens)
        {
            userTokens.RemoveAll(x =>
                !x.IsActive &&
                x.Created.AddDays(int.Parse(_configuration["AppSettings:RefreshTokenExpiryDays"])) <= DateTime.UtcNow);
        }

        private IEnumerable<Claim> getClaims(List<UserClaim> userClaims) {
            foreach (var t in userClaims) 
                yield return new Claim(t.Description, t.Value);
        }
    }
}
