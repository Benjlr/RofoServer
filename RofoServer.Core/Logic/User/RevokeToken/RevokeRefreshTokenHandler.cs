using MediatR;
using RofoServer.Core.Utils.TokenService;
using RofoServer.Domain.IdentityObjects;
using RofoServer.Domain.IRepositories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RofoServer.Core.Logic.RevokeToken
{
    public class RevokeRefreshTokenHandler : IRequestHandler<RevokeRefreshTokenCommand, RevokeRefreshTokenResponseModel>
    {
        private User _user;
        private RefreshToken _userRefreshToken;
        private readonly IRepositoryManager _repository;
        private readonly IJwtServices _tokenGenerator;

        public RevokeRefreshTokenHandler(
            IRepositoryManager users,
            IJwtServices tokenGen) {
            _repository = users;
            _tokenGenerator = tokenGen;
        }

        public async Task<RevokeRefreshTokenResponseModel> Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken) {
            await getUserAndToken(request.RefreshToken.Token);
            if (_userRefreshToken == null || _user == null)
                return new RevokeRefreshTokenResponseModel() { Errors = "INVALID_REFRESH_TOKEN" };

            _tokenGenerator.RevokeRefreshToken(_userRefreshToken, "Revoked without replacement");
            await _repository.UserRepository.UpdateAsync(_user);

            return new RevokeRefreshTokenResponseModel();
        }
        private async Task getUserAndToken(string userRefreshToken) {
            _user = await _repository.UserRepository.GetUserByRefreshTokenOrDefault(userRefreshToken);
            _userRefreshToken = _user?.RefreshTokens.Single(x => x.Token == userRefreshToken);
        }
        
    }
}
