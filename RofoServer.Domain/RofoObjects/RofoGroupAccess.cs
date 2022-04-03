using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RofoServer.Domain.RofoObjects;

[Index("User", "Group", IsUnique = true)]

public class RofoGroupAccess
{
    [Key] public int Id { get; set; }
    public int User { get; set; }
    public int Group { get; set; }
    public string Rights { get; set; }
}

