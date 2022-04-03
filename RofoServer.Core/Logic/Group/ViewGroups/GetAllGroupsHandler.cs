﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using RofoServer.Domain.IdentityObjects;
using RofoServer.Domain.IRepositories;

namespace RofoServer.Core.Logic.Group.ViewGroups
{
    public class GetAllGroupsHandler : IRequestHandler<GetGroupsCommand, GetAllGroupResponseModel>
    {
        private readonly IRepositoryManager _repo;
        private User _user;

        public GetAllGroupsHandler(IRepositoryManager repo, IConfiguration config) {
            _repo = repo;
        }

        public async Task<GetAllGroupResponseModel> Handle(GetGroupsCommand request, CancellationToken cancellationToken) {
            _user = await _repo.UserRepository.GetUserByEmail(request.Request.Email);
            if (_user == null)
                return new GetAllGroupResponseModel() { Errors = "INVALID USER" };

            return new GetAllGroupResponseModel()
            {
                Groups = await _repo.RofoGroupRepository.GetGroups(_user)
            };
        }
    }
}
