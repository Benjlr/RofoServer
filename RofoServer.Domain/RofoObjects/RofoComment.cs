using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RofoServer.Domain.IdentityObjects;

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
