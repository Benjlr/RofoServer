using MediatR;
using RofoServer.Core.Utils;
using RofoServer.Domain.IRepositories;
using RofoServer.Domain.RofoObjects;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RofoServer.Core.Rofo.CommentRofo;

public class CommentRofoHandler : IRequestHandler<CommentRofoCommand, CommentRofoResponseModel>
{
    private readonly IRepositoryManager _repo;
    private Domain.IdentityObjects.RofoUser _user;

    public CommentRofoHandler(IRepositoryManager repo) {
        _repo = repo;
    }

    public async Task<CommentRofoResponseModel> Handle(CommentRofoCommand request, CancellationToken cancellationToken) {
        _user = await _repo.UserRepository.GetUserByEmail(request.Request.Email);
        if (_user == null)
            return new CommentRofoResponseModel { Errors = "INVALID_USER" };

        var photo = await _repo.RofoRepository.GetByStamp(Guid.Parse(request.Request.PhotoId));
        if(photo == null)
            return new CommentRofoResponseModel { Errors = "INVALID_REQUEST" };

        var permission = await _repo.RofoGroupAccessRepository.GetGroupPermission(_user, photo.Group);
        if (permission.Rights != RofoClaims.READ_WRITE_GROUP_CLAIM)
            return new CommentRofoResponseModel { Errors = "INVALID_PERMISSION" };
        
        photo.Comments.Add(new RofoComment()
        {
            ParentPhoto = photo,
            Text = request.Request.Text,
            UploadedBy = _user,
            UploadedDateTime = DateTime.UtcNow,
            Visible = true
        });

        await _repo.RofoRepository.UpdateAsync(photo);
        await _repo.Complete();
        return new CommentRofoResponseModel();
    }
}