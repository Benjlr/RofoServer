using RofoServer.Domain.IdentityObjects;
using System.ComponentModel.DataAnnotations;

namespace RofoServer.Domain.RofoObjects;


public class RofoGroupAccess
{
    [Key] public int Id { get; set; }
    public virtual RofoUser User { get; set; }
    public virtual RofoGroup Group { get; set; }
    public string Rights { get; set; }
}

