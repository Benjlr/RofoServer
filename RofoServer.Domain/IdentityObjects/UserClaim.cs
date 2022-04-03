using System.ComponentModel.DataAnnotations;

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
