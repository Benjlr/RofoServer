using MediatR;

namespace RofoServer.Core.Logic.Group.ViewGroups
{
    public class GetGroupsCommand : IRequest<GetAllGroupResponseModel>
    {
        public GetAllGroupsRequestModel Request{ get; set; }

        public GetGroupsCommand(GetAllGroupsRequestModel request)
        {
            Request = request;
        }

    }
}
