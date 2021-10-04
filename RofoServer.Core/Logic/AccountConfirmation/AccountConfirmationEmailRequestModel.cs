using System.ComponentModel.DataAnnotations;
using MediatR;

namespace RofoServer.Core.Logic.AccountConfirmation
{
    public class AccountConfirmationEmailRequestModel : IRequest<AccountConfirmationEmailRequestModel>
    {
        [Required(ErrorMessage = "Email required"), EmailAddress(ErrorMessage = "Email is invalid format")]
        public string Email { get; set; }
        public string CallbackUrl { get; set; }
        public string ConfirmationEndpoint { get; set; }
    }
}
