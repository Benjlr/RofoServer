using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RofoServer.Domain.IdentityObjects
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public  virtual UserAuthentication UserAuthDetails { get; set; }
        public virtual List<RefreshToken> RefreshTokens { get; set; }
        public virtual List<UserClaim> UserClaims { get; set; }
    }
}
