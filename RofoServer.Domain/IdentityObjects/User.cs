using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RofoServer.Domain.IdentityObjects;

public class RofoUser
{
    [Key] public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public virtual UserAuthentication UserAuthDetails { get; set; }
    public virtual List<RefreshToken> RefreshTokens { get; set; }
    public virtual List<UserClaim> UserClaims { get; set; }

}