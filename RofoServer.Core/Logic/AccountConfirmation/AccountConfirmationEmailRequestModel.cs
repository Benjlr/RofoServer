using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Ubiquity.IdentityServer.Core.UseCases.AccountConfirmationEmail
{
    public class AccountConfirmationEmailRequestModel : IRequest<AccountConfirmationEmailRequestModel>
    {
        [Required(ErrorMessage = "Email required"), EmailAddress(ErrorMessage = "Email is invalid format")]
        public string Email { get; set; }

        public string CallbackUrl { get; set; }
        public string ConfirmationEndpoint { get; set; }
    }
}
