using MediatR;

namespace RofoServer.Core.User.RefreshTokenLogic
{
    public class RefreshTokenCommand : IRequest<RefreshTokenResponseModel>
    {
        public RefreshTokenRequestModel RefreshToken { get; set; }

        public RefreshTokenCommand(RefreshTokenRequestModel refreshToken) {
            RefreshToken = refreshToken;
        }

    }
}
