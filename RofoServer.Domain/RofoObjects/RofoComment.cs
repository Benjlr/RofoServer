using RofoServer.Domain.IdentityObjects;
using System;
using System.ComponentModel.DataAnnotations;

namespace RofoServer.Domain.RofoObjects
{
    public class RofoComment
    {
        [Key ]
        public int RofoCommentId { get; set; }
        public virtual RofoUser UploadedBy { get; set; }
        public virtual Rofo ParentPhoto { get; set; }
        public DateTime UploadedDateTime { get; set; }
        public bool Visible { get; set; }
        public string Text { get; set; }


    }
}
