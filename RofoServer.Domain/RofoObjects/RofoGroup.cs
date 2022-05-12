using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RofoServer.Domain.RofoObjects
{
    public class RofoGroup
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string StorageLocation { get; set; }
        public Guid SecurityStamp { get; set; }
    }
}