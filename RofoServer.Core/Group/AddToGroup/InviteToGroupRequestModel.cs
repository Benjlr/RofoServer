using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using MediatR;

namespace RofoServer.Core.Group.AddToGroup;

public class InviteToGroupRequestModel : IRequest<InviteToGroupRequestModel>
{
    [IgnoreDataMember]
    public string Email { get; set; }

    [Required(ErrorMessage = "Email required"), EmailAddress(ErrorMessage = "Email is invalid format")]
    public string NewMemberEmail { get; set; }

    [Required(ErrorMessage = "Group id required")]
    public Guid GroupId{ get; set; }

    [Required(ErrorMessage = "Access Level Required")]
    public string AccessLevel { get; set; }
    public string Message { get; set; }


    public string CallbackUrl { get; set; }
    [IgnoreDataMember]

    public string ConfirmationEndpoint { get; set; }
    public string RegisterEndpoint { get; set; }

}