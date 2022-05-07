using MediatR;
using Microsoft.Extensions.Configuration;
using RofoServer.Domain.IRepositories;
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
            Groups = await _repo.RofoGroupRepository.GetUsersGroups(_user)
        };
    }
}