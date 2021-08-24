using System.ComponentModel.DataAnnotations;
using static RofoServer.Core.Validators.Validation.Rofo;

namespace RofoServer.Core.RequestModels
{
    public class CreateRofoRequest
    {
        [Required]
        public byte[] image { get; set; }

        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }
    }
}
