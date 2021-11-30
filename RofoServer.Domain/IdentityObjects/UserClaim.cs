using System.ComponentModel.DataAnnotations;
using RofoServer.Domain.RofoObjects;

namespace RofoServer.Domain.IdentityObjects
{
    public class UserClaim
    {
        [Key]
        public int Id { get; set; }
        public virtual RofoGroup Group { get; set; }
        public string Description { get; set; }
    }

}
