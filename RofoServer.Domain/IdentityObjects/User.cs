using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RofoServer.Domain.IdentityObjects
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string PasswordHash { get; set; }

        [JsonIgnore]
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}
