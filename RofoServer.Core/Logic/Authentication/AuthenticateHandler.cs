using MediatR;
using RofoServer.Core.Utils;
using RofoServer.Core.Utils.TokenService;
using RofoServer.Domain.IRepositories;
using System.Threading;
using System.Threading.Tasks;

namespace RofoServer.Core.Logic.Authentication
{
    public class AuthenticateHandler : IRequestHandler<AuthenticationCommand, AuthenticateResponseModel>
    {
        private readonly IUserRepository _repo;
        private readonly ITokenServices _tokenService;

        public AuthenticateHandler(IUserRepository repo, ITokenServices tokenService) {
            _repo = repo;
            _tokenService = tokenService;
        }

        public async Task<AuthenticateResponseModel> Handle(AuthenticationCommand request, CancellationToken cancellationToken) {
            var user = await _repo.GetUserByEmail(request.Request.Email);

            if (!PasswordHasher.CheckPassword(user.PasswordHash, request.Request.Password))
                return new AuthenticateResponseModel() {Errors = "INVALID_CREDENTIALS"};

            return new AuthenticateResponseModel()
            {
                Email = user.Email,
                Username = user.UserName,
                JwtToken = _tokenService.GenerateJwtToken(user.Email),
                RefreshToken = _tokenService.GenerateRefreshToken().Token
            };

        }
    }
}
