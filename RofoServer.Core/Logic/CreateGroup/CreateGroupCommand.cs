using MediatR;

namespace RofoServer.Core.Logic.CreateGroup
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
