using MediatR;
using RofoServer.Core.Group.ViewGroups;
using RofoServer.Core.Rofo.GetAllComments;
using RofoServer.Core.Rofo.ViewRofos;
using RofoServer.Core.Utils;
using RofoServer.Domain.IdentityObjects;
using RofoServer.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RofoServer.Core.Rofo.DownloadRofo;

public class DownloadRofoHandler : IRequestHandler<DownloadRofoCommand, DownloadRofoResponseModel>
{
    private readonly IRepositoryManager _repo;
    private IBlobService _blobber;
    private RofoUser _user;

    public DownloadRofoHandler(IRepositoryManager repo, IBlobService blobService) {
        _repo = repo;
        _blobber = blobService;
    }

    public async Task<DownloadRofoResponseModel> Handle(DownloadRofoCommand request, CancellationToken cancellationToken) {
        _user = await _repo.UserRepository.GetUserByEmail(request.Request.Email);
        if (_user == null)
            return new DownloadRofoResponseModel { Errors = "INVALID_USER" };

        var rofo = await _repo.RofoRepository.GetByStamp(Guid.Parse(request.Request.PhotoId));

        var permission = await _repo.RofoGroupAccessRepository.GetGroupPermission(_user, rofo.Group.SecurityStamp);
        if (permission == null ||
            (permission.Rights != RofoClaims.READ_WRITE_GROUP_CLAIM &&
             permission.Rights != RofoClaims.READ_GROUP_CLAIM))
            return new DownloadRofoResponseModel() { Errors = "INVALID_REQUEST" };

        await _repo.RofoRepository.AddAsync(rofo);
        await _repo.Complete();
        return new DownloadRofoResponseModel()
        {
            Photo = GetRofos(rofo)
        };
    }

    private RofoResponseModel GetRofos(Domain.RofoObjects.Rofo rofo) {

        return new RofoResponseModel()
        {
            Comments = GetComments(rofo).ToList(),
            Description = rofo.Description,
            PhotoUploadedByUserName = rofo.UploadedBy.UserName,
            SecurityStamp = rofo.SecurityStamp,
            UploadedDate = rofo.UploadedDate,
            Group = new GroupResponse()
            {
                Description = rofo.Group.Description,
                Name = rofo.Group.Name,
                SecurityStamp = rofo.Group.SecurityStamp
            }
        };
    }


    private IEnumerable<CommentResponse> GetComments(Domain.RofoObjects.Rofo photo) {
        for (int i = 0; i < photo.Comments.Count; i++) {
            yield return new CommentResponse()
            {
                ParentPhoto = photo.SecurityStamp,
                Text = photo.Comments[i].Text,
                UploadedDateTime = photo.Comments[i].UploadedDateTime,
                UploadedByUserName = photo.Comments[i].UploadedBy.UserName
            };
        }
    }
}