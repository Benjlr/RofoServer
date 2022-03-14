using MediatR;
using Microsoft.Extensions.Configuration;
using RofoServer.Core.Utils;
using RofoServer.Domain.IdentityObjects;
using RofoServer.Domain.IRepositories;
using RofoServer.Domain.RofoObjects;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RofoServer.Core.Logic.CreateGroup
{
    public class CreateGroupHandler : IRequestHandler<CreateGroupCommand, CreateGroupResponseModel>
    {
        private readonly IRepositoryManager _repo;
        private User _user;

        public CreateGroupHandler(IRepositoryManager repo, IConfiguration config) {
            _repo = repo;
        }

        public async Task<CreateGroupResponseModel> Handle(CreateGroupCommand request, CancellationToken cancellationToken) {
            _user = await _repo.UserRepository.GetUserByEmail(request.Request.Email);
            
            var group = new RofoGroup()
            {
                Description = request.Request.Description,
                Name = request.Request.GroupName,
                SecurityStamp = Guid.NewGuid()
            };

            var userClaim = new UserClaim()
            {
                Description = RofoClaims.ReadWrite,
                Group = group
            };

            await _repo.RofoGroupRepository.AddAsync(group);
            _user.UserClaims.Add(userClaim);

            await _repo.Complete();
            return new CreateGroupResponseModel();
        }
    }
}
