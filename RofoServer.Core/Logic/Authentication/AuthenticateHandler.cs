using MediatR;
using RofoServer.Core.ResponseModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using RofoServer.Core.Logic.TokenService;
using RofoServer.Domain;
using Microsoft.Extensions.Configuration;

namespace RofoServer.Core.Logic.Authentication
{
    public class AuthenticateHandler : IRequestHandler<AuthenticateQuery, AuthenticateResponseModel>
    {
        private IRepository _repo;
        private ITokenService _tokenService;
        private IConfiguration _config;

        public AuthenticateHandler(IRepository repo, ITokenService tokenService, IConfiguration config) {
            _repo = repo;
            _tokenService = tokenService;
            _config = config;
        }

        public async Task<AuthenticateResponseModel> Handle(AuthenticateQuery request, CancellationToken cancellationToken) {
            var user = await _repo.GetUserByEmail(request.Request.Email);

            if (!PasswordHasher.CheckPassword(user.PasswordHash, request.Request.Password))
                return new AuthenticateResponseModel() {Errors = "INVALID_CREDENTIALS"};

            return new AuthenticateResponseModel()
            {
                Email = user.Email,
                Username = user.UserName,
                JwtToken = _tokenService.GenerateJWTToken(new List<Claim>()
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.UserName),
                }, _config["Rofos:ApiKey"]),
                RefreshToken = _tokenService.GenerateRefreshToken(request.Request.)
            };

        }
    }
}
