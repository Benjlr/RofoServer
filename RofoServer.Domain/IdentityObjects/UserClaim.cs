using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace RofoServer.Domain.IdentityObjects
{
    [Owned]

    public class UserClaim
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
    }

}
