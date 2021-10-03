using System.ComponentModel.DataAnnotations;
using MediatR;

namespace RofoServer.Core.Logic.ValidateAccount
{
    public class ValidateAccountRequestModel : IRequest<ValidateAccountRequestModel>
    {
        [Required(ErrorMessage = "Email required"), EmailAddress(ErrorMessage = "Email is invalid format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "No validation code attached")]
        public string ValidationCode { get; set; }
        public string CallbackUrl { get; set; }

    }
}
