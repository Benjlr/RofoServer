using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RofoServer.Domain.IdentityObjects
{
    public class Claims
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
