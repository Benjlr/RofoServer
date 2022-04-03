using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace RofoServer.Domain.IdentityObjects
{
    public class UserClaim
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }

}
