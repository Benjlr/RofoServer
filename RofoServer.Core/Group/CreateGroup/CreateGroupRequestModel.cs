using System.ComponentModel.DataAnnotations;
using MediatR;

namespace RofoServer.Core.Group.CreateGroup;

public class CreateGroupRequestModel : IRequest<CreateGroupRequestModel>
{
    public string Email { get; set; }

    [Required(ErrorMessage = "Group name required")]
    public string GroupName { get; set; }

    [Required(ErrorMessage = "Description required")]
    public string Description { get; set; }

}