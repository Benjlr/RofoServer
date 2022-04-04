using MediatR;
using RofoServer.Core.Utils;
using RofoServer.Domain.IRepositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RofoServer.Core.Group.JoinGroup;

public class JoinGroupHandler : IRequestHandler<JoinGroupCommand, JoinGroupResponseModel>
{
    private readonly IRepositoryManager _repo;
    private readonly ITokenGenerator _tokenGenerator;

    public JoinGroupHandler(
        IRepositoryManager repo,
        ITokenGenerator tokenGenerator) {
        _repo = repo;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<JoinGroupResponseModel> Handle(JoinGroupCommand request, CancellationToken cancellationToken) {
        var user = await _repo.UserRepository.GetUserByEmail(request.Request.Email);
        if (user == null)
            return new JoinGroupResponseModel() { Errors = "INVALID_REQUEST" };

        var parsedToken = await _tokenGenerator.ValidateAsync(user.Email, request.Request.ValidationCode);
        if (parsedToken == null)
            return new JoinGroupResponseModel() { Errors = "ACCOUNT_CODE_INVALID" };

        var group = await _repo.RofoGroupRepository.SingleOrDefaultAsync(x => x.SecurityStamp.Equals(new Guid(parsedToken.Item1)));
        if(group == null)
            return new JoinGroupResponseModel() { Errors = "ACCOUNT_CODE_INVALID" };

        await _repo.RofoGroupAccessRepository.AddOrUpdateGroupClaimAsync(group, user, parsedToken.Item2);
        await _repo.Complete();
        return new JoinGroupResponseModel();
    }

}