using System;
using System.ComponentModel.DataAnnotations;

namespace RofoServer.Domain.RofoObjects;
public  class PendingRofoAccess
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Rights { get; set; }
        public virtual RofoGroup Group { get; set; }
        public DateTime Created { get; set; }

    }

