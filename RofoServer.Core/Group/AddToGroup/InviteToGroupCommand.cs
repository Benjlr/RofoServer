using MediatR;

namespace RofoServer.Core.Group.AddToGroup;

public class InviteToGroupCommand : IRequest<InviteToGroupGroupResponseModel>
{
    public InviteToGroupRequestModel Request { get; set; }

    public InviteToGroupCommand(InviteToGroupRequestModel req) {
        Request = req;
    }

}