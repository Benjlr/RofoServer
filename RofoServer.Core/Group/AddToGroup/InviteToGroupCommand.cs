using MediatR;

namespace RofoServer.Core.Group.AddToGroup;

public class InviteToGroupCommand : IRequest<InviteToGroupResponseModel>
{
    public InviteToGroupRequestModel Request { get; set; }

    public InviteToGroupCommand(InviteToGroupRequestModel req) {
        Request = req;
    }

}