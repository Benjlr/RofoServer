using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RofoServer.Core.Utils;
using RofoServer.Domain.IRepositories;

namespace RofoServer.Core.Rofo.GetAllComments;

public class GetAllCommentsRofoHandler : IRequestHandler<GetAllCommentsRofoCommand, GetAllCommentsRofoResponseModel>
{
    private readonly IRepositoryManager _repo;
    private Domain.IdentityObjects.RofoUser _user;

    public GetAllCommentsRofoHandler(IRepositoryManager repo) {
        _repo = repo;
    }

    public async Task<GetAllCommentsRofoResponseModel> Handle(GetAllCommentsRofoCommand request, CancellationToken cancellationToken) {
        _user = await _repo.UserRepository.GetUserByEmail(request.Request.Email);
        if (_user == null)
            return new GetAllCommentsRofoResponseModel { Errors = "INVALID_USER" };

        var photo = await _repo.RofoRepository.GetByStamp(Guid.Parse(request.Request.PhotoId));
        if(photo == null)
            return new GetAllCommentsRofoResponseModel { Errors = "INVALID_REQUEST" };

        var permission = await _repo.RofoGroupAccessRepository.GetGroupPermission(_user, photo.Group);
        if (permission == null ||
            (permission.Rights != RofoClaims.READ_GROUP_CLAIM &&
             permission.Rights != RofoClaims.READ_WRITE_GROUP_CLAIM))
            return new GetAllCommentsRofoResponseModel() { Errors = "INVALID_REQUEST" };
        

        await _repo.RofoRepository.UpdateAsync(photo);
        await _repo.Complete();
        return new GetAllCommentsRofoResponseModel()
        {
            Comments = photo.Comments
        };
    }
}