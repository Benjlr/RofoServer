using System.ComponentModel.DataAnnotations;
using MediatR;

namespace RofoServer.Core.Logic.Register
{
    public class RegisterRequestModel : IRequest<RegisterRequestModel>
    {
        [Required(ErrorMessage = "Email required"), EmailAddress(ErrorMessage = "Email is invalid format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "No password attached")]
        public string Password { get; set; }

        [Required(ErrorMessage = "No username")]
        public string Username { get; set; }

    }
}
