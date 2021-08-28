using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace RofoServer.Domain.IdentityObjects
{
    [Owned]
    public class UserAuthentication
    {
        [Key]
        public int Id { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool AccountConfirmed { get; set; }
        public DateTime LockOutExpiry { get; set; }
        public int FailedLogInAttempts { get; set; }
        [JsonIgnore]
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}