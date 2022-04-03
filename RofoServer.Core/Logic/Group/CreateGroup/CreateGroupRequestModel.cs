using System.ComponentModel.DataAnnotations;
using MediatR;

namespace RofoServer.Core.Logic.Group.CreateGroup
{
    public class CreateGroupRequestModel : IRequest<CreateGroupRequestModel>
    {
        [Required(ErrorMessage = "Email required"), EmailAddress(ErrorMessage = "Email is invalid format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Group name required")]
        public string GroupName { get; set; }

        [Required(ErrorMessage = "Description required")]
        public string Description { get; set; }

    }
}
