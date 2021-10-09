using System;
using System.Collections.Generic;
using MediatR;
using RofoServer.Core.Utils;
using RofoServer.Domain.IdentityObjects;
using RofoServer.Domain.IRepositories;
using System.Threading;
using System.Threading.Tasks;

namespace RofoServer.Core.Logic.Register
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, RegisterResponseModel>
    {
        private readonly IRepositoryManager _manager;

        public RegisterHandler(IRepositoryManager manager) {
            _manager = manager;
        }

        public async Task<RegisterResponseModel> Handle(RegisterCommand request, CancellationToken cancellationToken) {
            if (await _manager.UserRepository.GetUserByEmail(request.Request.Email) != null)
                return new RegisterResponseModel() {Errors = "USER_EXISTS"};
            var result = await _manager.UserRepository.AddAsync(new User
            {
                UserName = request.Request.Username,
                Email = request.Request.Email,
                PasswordHash = PasswordHasher.HashPassword(request.Request.Password),
                UserAuthDetails = new UserAuthentication()
                {
                    TwoFactorEnabled = false,
                    AccountConfirmed = false,
                    SecurityStamp = Guid.NewGuid(),
                },
                UserClaims = new List<UserClaim>(){new (){Description = RofoClaims.UserClaim, Value = "basic"}}
            });
            if (result == 1) {

            }

            await _manager.Complete();
            return result == 0 ? 
                new RegisterResponseModel() { Errors = "SERVER_ERROR" } : 
                new RegisterResponseModel();
        }
    }

    public class RofoClaims
    {
        public static string UserClaim = "RofoUser";

    }

}
