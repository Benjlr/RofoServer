using RofoServer.Domain.IdentityObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RofoServer.Domain.RofoObjects
{
    public class Rofo
    {
        [Key]
        public int RofoId { get; set; }
        public string ImageUrl { get; set; }
        public string FileMetaData { get; set; }
        public string Description { get; set; }
        public bool Visible { get; set; }
        public virtual RofoUser UploadedBy { get; set; }
        public virtual RofoGroup Group { get; set; }
        public virtual List<RofoComment> Comments{ get; set; }
        public DateTime UploadedDate { get; set; }
        public Guid SecurityStamp { get; set; }


    }
}
