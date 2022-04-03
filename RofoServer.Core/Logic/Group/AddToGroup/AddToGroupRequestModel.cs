using System.ComponentModel.DataAnnotations;
using MediatR;

namespace RofoServer.Core.Logic.Group.AddToGroup
{
    public class AddToGroupRequestModel : IRequest<AddToGroupRequestModel>
    {
        [Required(ErrorMessage = "Email required"), EmailAddress(ErrorMessage = "Email is invalid format")]
        public string NewMemberEmail { get; set; }

        [Required(ErrorMessage = "Group name required")]
        public string GroupName { get; set; }

    }
}
