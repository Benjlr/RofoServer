using MediatR;
using RofoServer.Core.Utils;
using RofoServer.Domain.IRepositories;
using System.Threading;
using System.Threading.Tasks;

namespace RofoServer.Core.Rofo.ViewRofos;

public class ViewRofosHandler : IRequestHandler<ViewRofosCommand, ViewRofosResponseModel> {
    private readonly IRepositoryManager _repo;
    private Domain.IdentityObjects.RofoUser _user;

    public ViewRofosHandler(IRepositoryManager repo) {
        _repo = repo;
    }

    public async Task<ViewRofosResponseModel> Handle(ViewRofosCommand request, CancellationToken cancellationToken) {
        _user = await _repo.UserRepository.GetUserByEmail(request.Request.Email);
        if (_user == null)
            return new ViewRofosResponseModel {Errors = "INVALID_REQUEST"};

        var permission = await _repo.RofoGroupAccessRepository.GetGroupPermission(_user, request.Request.GroupId);
        if (permission.Rights != RofoClaims.READ_WRITE_GROUP_CLAIM &&
            permission.Rights != RofoClaims.READ_GROUP_CLAIM)
            return new ViewRofosResponseModel() {Errors = "INVALID_REQUEST"};

        var rofos = await _repo.RofoRepository.FindAllAsync(x => x.Group.SecurityStamp.Equals(request.Request.GroupId));
        return new ViewRofosResponseModel() {
            Rofos = rofos
        };
    }
}