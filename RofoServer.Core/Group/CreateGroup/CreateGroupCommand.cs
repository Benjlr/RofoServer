using MediatR;

namespace RofoServer.Core.Group.CreateGroup
{
    public class CreateGroupCommand : IRequest<CreateGroupResponseModel>
    {
        public CreateGroupRequestModel Request { get; set; }

        public CreateGroupCommand(CreateGroupRequestModel req)
        {
            Request = req;
        }

    }
}
