using System.ComponentModel.DataAnnotations;
using MediatR;

namespace RofoServer.Core.Group.AddToGroup;

public class InviteToGroupRequestModel : IRequest<InviteToGroupRequestModel>
{
    public string Email { get; set; }

    [Required(ErrorMessage = "Email required"), EmailAddress(ErrorMessage = "Email is invalid format")]
    public string NewMemberEmail { get; set; }

    [Required(ErrorMessage = "Group id required")]
    public int GroupId{ get; set; }

    [Required(ErrorMessage = "Access Level Required")]
    public string AccessLevel { get; set; }

    public string CallbackUrl { get; set; }
    public string ConfirmationEndpoint { get; set; }
    public string RegisterEndpoint { get; set; }

}