using MediatR;

namespace RofoServer.Core.Group.ViewGroups;

public class GetAllGroupsRequestModel : IRequest<GetAllGroupsRequestModel>
{
    public string Email { get; set; }
}