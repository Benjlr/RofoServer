using System.ComponentModel.DataAnnotations;
using MediatR;

namespace RofoServer.Core.Logic.RevokeToken
{
    public class RevokeRefreshTokenRequestModel : IRequest<RevokeRefreshTokenResponseModel>
    {
        [Required(ErrorMessage = "No refresh token attached")]
        public string Token { get; set; }
    }
}
