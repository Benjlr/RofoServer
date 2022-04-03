using System.ComponentModel.DataAnnotations;
using MediatR;

namespace RofoServer.Core.User.AccountConfirmation
{
    public class AccountConfirmationEmailRequestModel : IRequest<AccountConfirmationEmailRequestModel>
    {
        [Required(ErrorMessage = "Email required"), EmailAddress(ErrorMessage = "Email is invalid format")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password required")]
        public string Password { get; set; }
        public string CallbackUrl { get; set; }
        public string ConfirmationEndpoint { get; set; }
    }
}
