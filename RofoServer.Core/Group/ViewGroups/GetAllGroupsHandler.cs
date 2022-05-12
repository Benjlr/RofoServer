using MediatR;
using Microsoft.Extensions.Configuration;
using RofoServer.Domain.IRepositories;
using RofoServer.Domain.RofoObjects;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RofoServer.Core.Group.ViewGroups;

public class GetAllGroupsHandler : IRequestHandler<GetGroupsCommand, GetAllGroupResponseModel>
{
    private readonly IRepositoryManager _repo;

    public GetAllGroupsHandler(IRepositoryManager repo, IConfiguration config) {
        _repo = repo;
    }

    public async Task<GetAllGroupResponseModel> Handle(GetGroupsCommand request, CancellationToken cancellationToken) {
        var _user = await _repo.UserRepository.GetUserByEmail(request.Request.Email);
        if (_user == null)
            return new GetAllGroupResponseModel() { Errors = "INVALID USER" };
        

        return new GetAllGroupResponseModel()
        {
            Groups = GetGroups(await _repo.RofoGroupRepository.GetUsersGroups(_user)).ToList()
        };
    }

    private IEnumerable<GroupResponse> GetGroups(List<RofoGroup> groups) {
        for (int i = 0; i < groups.Count; i++) {
            yield return new GroupResponse()
            {
                Description = groups[i].Description,
                Name = groups[i].Name,
                SecurityStamp = groups[i].SecurityStamp
            };
        }
    }
}