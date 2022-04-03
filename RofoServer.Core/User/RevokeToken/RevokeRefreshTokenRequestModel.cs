using System.ComponentModel.DataAnnotations;
using MediatR;

namespace RofoServer.Core.User.RevokeToken
{
    public class RevokeRefreshTokenRequestModel : IRequest<RevokeRefreshTokenResponseModel>
    {
        [Required(ErrorMessage = "No refresh token attached")]
        public string Token { get; set; }
    }
}
