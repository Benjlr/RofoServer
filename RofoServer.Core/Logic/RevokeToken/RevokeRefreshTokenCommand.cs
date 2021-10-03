using MediatR;

namespace RofoServer.Core.Logic.RevokeToken
{
    public class RevokeRefreshTokenCommand : IRequest<RevokeRefreshTokenResponseModel>
    {
        public RevokeRefreshTokenRequestModel RefreshToken { get; set; }
        
        public RevokeRefreshTokenCommand(RevokeRefreshTokenRequestModel refreshToken) {
            RefreshToken = refreshToken;
        }

    }
}
