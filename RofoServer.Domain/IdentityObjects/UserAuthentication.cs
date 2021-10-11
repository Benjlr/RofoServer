using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RofoServer.Domain.IdentityObjects
{
    public class UserAuthentication
    {
        [Key]
        public int Id { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool AccountConfirmed { get; set; }
        public DateTime LockOutExpiry { get; set; }
        public int FailedLogInAttempts { get; set; }
        public Guid SecurityStamp { get; set; }

    }
}