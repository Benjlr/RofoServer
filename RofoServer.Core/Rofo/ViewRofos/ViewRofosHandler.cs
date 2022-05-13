using MediatR;
using RofoServer.Core.Rofo.GetAllComments;
using RofoServer.Core.Utils;
using RofoServer.Domain.IRepositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RofoServer.Core.Group.ViewGroups;

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
        if (permission == null || 
            (permission.Rights != RofoClaims.READ_WRITE_GROUP_CLAIM &&
            permission.Rights != RofoClaims.READ_GROUP_CLAIM))
            return new ViewRofosResponseModel() {Errors = "INVALID_REQUEST"};

        var rofos = await _repo.RofoRepository.FindAllAsync(x => x.Group.SecurityStamp.Equals(request.Request.GroupId));
        return new ViewRofosResponseModel() {
            Rofos = GetRofos(rofos).ToList()
        };
    }

    private IEnumerable<RofoResponseModel> GetRofos(List<Domain.RofoObjects.Rofo> rofos) {
        for (int i = 0; i < rofos.Count; i++) {
            yield return new RofoResponseModel()
            {
                Comments = GetComments(rofos[i]).ToList(),
                Description = rofos[i].Description,
                PhotoUploadedByUserName = rofos[i].UploadedBy.UserName,
                SecurityStamp = rofos[i].SecurityStamp,
                UploadedDate = rofos[i].UploadedDate,
                Group = new GroupResponse()
                {
                    Description = rofos[i].Group.Description,
                    Name = rofos[i].Group.Name,
                    SecurityStamp = rofos[i].Group.SecurityStamp
                }
            };
        }
    }

    private IEnumerable<CommentResponse> GetComments(Domain.RofoObjects.Rofo photo)
    {
        for (int i = 0; i < photo.Comments.Count; i++)
        {
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