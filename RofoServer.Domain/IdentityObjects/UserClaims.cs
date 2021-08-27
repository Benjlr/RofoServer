using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RofoServer.Domain.IdentityObjects
{
    public class UserClaims
    {
        [Key] 
        [JsonIgnore]
        public int UserClaimsId { get; set; }
        public virtual User User { get; set; }
        public virtual Claims Claim { get; set; }
    }
}
