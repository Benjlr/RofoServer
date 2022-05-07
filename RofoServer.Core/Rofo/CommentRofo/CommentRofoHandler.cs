using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using RofoServer.Core.Utils;
using RofoServer.Domain.IRepositories;
using RofoServer.Domain.RofoObjects;

namespace RofoServer.Core.Rofo.CommentRofo;

public class CommentRofoHandler : IRequestHandler<CommentRofoCommand, CommentRofoResponseModel>
{
    private readonly IRepositoryManager _repo;
    private IBlobService _blobber;
    private Domain.IdentityObjects.RofoUser _user;

    public CommentRofoHandler(IRepositoryManager repo, IConfiguration config,IBlobService blobService) {
        _repo = repo;
        _blobber = blobService;
    }

    public async Task<CommentRofoResponseModel> Handle(CommentRofoCommand request, CancellationToken cancellationToken) {
        _user = await _repo.UserRepository.GetUserByEmail(request.Request.Email);
        if (_user == null)
            return new CommentRofoResponseModel { Errors = "INVALID USER" };

        var group = new RofoGroup()
        {
            Description = request.Request.Description,
            Name = request.Request.GroupName,
            SecurityStamp = Guid.NewGuid(),
            StorageLocation = await _blobber.CreateDirectory()
        };
        await _repo.RofoGroupRepository.AddAsync(group);
        await _repo.RofoGroupAccessRepository.AddOrUpdateGroupClaimAsync(group, _user, RofoClaims.READ_WRITE_GROUP_CLAIM);

        await _repo.Complete();
        return new CommentRofoResponseModel();
    }
}