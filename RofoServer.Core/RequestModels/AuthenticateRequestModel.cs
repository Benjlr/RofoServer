using System.ComponentModel.DataAnnotations;

namespace RofoServer.Core.RequestModels
{
    public class AuthenticateRequestModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
