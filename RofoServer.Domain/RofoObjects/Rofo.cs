﻿using System;
using System.ComponentModel.DataAnnotations;
using RofoServer.Domain.IdentityObjects;

namespace RofoServer.Domain.RofoObjects
{
    public class Rofo
    {
        [Key]
        public int RofoId { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public virtual User UploadedBy { get; set; }
        public DateTime UploadedDate { get; set; }


    }
}