using System.ComponentModel.DataAnnotations;
using MediatR;

namespace RofoServer.Core.Logic.RefreshTokenLogic
{
    public class RefreshTokenRequestModel : IRequest<RefreshTokenRequestModel>
    {
        [Required(ErrorMessage = "No refresh token attached")]
        public string Token { get; set; }
    }
}
