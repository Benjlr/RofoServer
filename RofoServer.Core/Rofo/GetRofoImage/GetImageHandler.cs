using MediatR;
using RofoServer.Core.Utils;
using RofoServer.Domain.IRepositories;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace RofoServer.Core.Rofo.GetRofoImage;

public class GetImageHandler : IRequestHandler<GetImageCommand, GetImageResponseModel> {
    private readonly IRepositoryManager _repo;
    private IBlobService _blobber;
    private Domain.IdentityObjects.RofoUser _user;

    public GetImageHandler(IRepositoryManager repo, IBlobService blobber) {
        _repo = repo;
        _blobber = blobber;
    }

    public async Task<GetImageResponseModel> Handle(GetImageCommand request, CancellationToken cancellationToken) {
        _user = await _repo.UserRepository.GetUserByEmail(request.Request.Email);
        if (_user == null)
            return new GetImageResponseModel { Errors = "INVALID_REQUEST"};

        var _photo = await _repo.RofoRepository.GetByStamp(request.Request.PhotoId);
        if(_photo == null)
            return new GetImageResponseModel { Errors = "INVALID_REQUEST" };

        var permission = await _repo.RofoGroupAccessRepository.GetGroupPermission(_user, _photo.Group);
        if (permission.Rights != RofoClaims.READ_GROUP_CLAIM)
            return new GetImageResponseModel() {Errors = "INVALID_REQUEST"};

        var myStream = new MemoryStream();
        await _blobber.DownloadPhoto(_photo.Group.StorageLocation, _photo.ImageUrl, myStream);
        return new GetImageResponseModel() {
            Data = Convert.ToBase64String(myStream.ToArray())
        };
    }
}