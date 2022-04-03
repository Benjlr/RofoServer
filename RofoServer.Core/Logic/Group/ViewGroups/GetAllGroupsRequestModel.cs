using MediatR;

namespace RofoServer.Core.Logic.Group.ViewGroups
{
    public class GetAllGroupsRequestModel : IRequest<GetAllGroupsRequestModel>
    {
        public string Email{ get; set; }
    }
}
