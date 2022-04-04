using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RofoServer.Core.Utils;
using RofoServer.Domain.IdentityObjects;
using RofoServer.Domain.IRepositories;

namespace RofoServer.Core.User.Register;

public class RegisterHandler : IRequestHandler<RegisterCommand, RegisterResponseModel>
{
    private readonly IRepositoryManager _manager;

    public RegisterHandler(IRepositoryManager manager) {
        _manager = manager;
    }

    public async Task<RegisterResponseModel> Handle(RegisterCommand request, CancellationToken cancellationToken) {
        if (await _manager.UserRepository.GetUserByEmail(request.Request.Email) != null)
            return new RegisterResponseModel() { Errors = "USER_EXISTS" };
        var result = await _manager.UserRepository.AddAsync(new RofoUser
        {
            UserName = request.Request.Username,
            Email = request.Request.Email,
            PasswordHash = PasswordHasher.HashPassword(request.Request.Password),
            UserAuthDetails = new UserAuthentication()
            {
                TwoFactorEnabled = false,
                AccountConfirmed = false,
                SecurityStamp = Guid.NewGuid(),
            }
        });

        await _manager.Complete();
        return new RegisterResponseModel();
    }
}