using MediatR;
using Microsoft.Extensions.Configuration;
using RofoServer.Core.Utils;
using RofoServer.Domain.IdentityObjects;
using RofoServer.Domain.IRepositories;
using RofoServer.Domain.RofoObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace RofoServer.Core.Rofo.UploadRofo;

public class UploadRofoHandler : IRequestHandler<UploadRofoCommand, UploadRofoResponseModel>
{
    private readonly IRepositoryManager _repo;
    private IBlobService _blobber;
    private RofoUser _user;

    public UploadRofoHandler(IRepositoryManager repo, IBlobService blobService) {
        _repo = repo;
        _blobber = blobService;
    }

    public async Task<UploadRofoResponseModel> Handle(UploadRofoCommand request, CancellationToken cancellationToken) {
        _user = await _repo.UserRepository.GetUserByEmail(request.Request.Email);
        if (_user == null)
            return new UploadRofoResponseModel { Errors = "INVALID_USER" };

        var permission = await _repo.RofoGroupAccessRepository.GetGroupPermission(_user, request.Request.GroupId);
        if (permission.Rights != RofoClaims.READ_WRITE_GROUP_CLAIM)
            return new UploadRofoResponseModel { Errors = "INVALID_PERMISSION" };

        var group = await _repo.RofoGroupRepository.GetGroupById(request.Request.GroupId);
        if(group == null)
            return new UploadRofoResponseModel { Errors = "FATAL_ERROR" };

        var data = request.Request.Photo.Split(',');
        var location = await _blobber.UploadPhoto(
            new MemoryStream(Convert.FromBase64String(data[1])),
            group.StorageLocation);

        if(string.IsNullOrWhiteSpace(location))
            return new UploadRofoResponseModel { Errors = "UPLOAD_ERROR" };

        var rofo = new Domain.RofoObjects.Rofo()
        {
            SecurityStamp = Guid.NewGuid(),
            Comments = new List<RofoComment>(),
            Description = request.Request.Description,
            FileMetaData = data[0],
            Group = group,
            ImageUrl = location,
            UploadedBy = _user,
            UploadedDate = DateTime.UtcNow,
            Visible = true
        };

        await _repo.RofoRepository.AddAsync(rofo);
        await _repo.Complete();
        return new UploadRofoResponseModel();
    }
}