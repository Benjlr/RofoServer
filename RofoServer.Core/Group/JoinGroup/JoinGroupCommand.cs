using MediatR;

namespace RofoServer.Core.Group.JoinGroup;

public class JoinGroupCommand : IRequest<JoinGroupResponseModel>
{
    public JoinGroupRequestModel Request { get; set; }

    public JoinGroupCommand(JoinGroupRequestModel req) {
        Request = req;
    }

}