using System.ComponentModel.DataAnnotations;
using MediatR;

namespace RofoServer.Core.Logic.Authentication
{
    public class AuthenticateRequestModel : IRequest<AuthenticateRequestModel>
    {
        [Required(ErrorMessage = "Email required"), EmailAddress(ErrorMessage = "Email is invalid format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Authenticator code required")]
        public string AuthenticatorCode { get; set; }
    }
}
