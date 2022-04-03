using RofoServer.Domain.IdentityObjects;
using System.ComponentModel.DataAnnotations;

namespace RofoServer.Domain.RofoObjects
{
    public class RofoGroupAccess
    {
        [Key]
        public virtual User User { get; set; }
        [Key]
        public virtual RofoGroup Group { get; set; }
        public string Rights { get; set; }
    }
}
