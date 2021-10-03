using MediatR;
using RofoServer.Core.Utils.TokenService;
using RofoServer.Domain.IdentityObjects;
using RofoServer.Domain.IRepositories;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace RofoServer.Core.Logic.RefreshTokenLogic
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenResponseModel>
    {
        private User _user;
        private RefreshToken _userRefreshToken;
        private readonly IRepositoryManager _repository;
        private readonly IJwtServices _tokenGenerator;

        public RefreshTokenHandler(
            IRepositoryManager repository,
            IJwtServices tokenGen) {
            _repository = repository;            
            _tokenGenerator = tokenGen;
        }

        public async Task<RefreshTokenResponseModel> Handle(RefreshTokenCommand request, CancellationToken cancellationToken) {
            await getUserAndToken(request.RefreshToken.Token);

            if(_userRefreshToken == null || _user == null || !await isActive(_userRefreshToken))
                return new RefreshTokenResponseModel() { Errors = "INVALID_REFRESH_TOKEN" };

            await changeAndSaveToken();

            return new RefreshTokenResponseModel() {
                Email = _user.Email,
                JwtToken = _tokenGenerator.GenerateJwtToken(_user.UserClaims),
                RefreshToken = _user.RefreshTokens.Last().Token
            };
        }

        private async Task getUserAndToken(string userRefreshToken) {
            _user = await _repository.UserRepository.GetUserByRefreshTokenOrDefault(userRefreshToken);
            _userRefreshToken = _user?.RefreshTokens.Single(x => x.Token == userRefreshToken);
        }

        private async Task changeAndSaveToken() {
            _tokenGenerator.RotateRefreshToken(_user.RefreshTokens);
            await _repository.UserRepository.UpdateAsync(_user);
        }


        private async Task<bool> isActive(RefreshToken token) {
            if (!token.IsActive) {
                _tokenGenerator.RevokeDescendantRefreshTokens(token, _user.RefreshTokens, $"Attempted reuse of revoked ancestor token: {token}");
                await _repository.UserRepository.UpdateAsync(_user);
            }
            return token.IsActive;
        }
    }
}
