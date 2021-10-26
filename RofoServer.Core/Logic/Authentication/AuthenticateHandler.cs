using MediatR;
using Microsoft.Extensions.Configuration;
using OtpNet;
using RofoServer.Core.Utils.TokenService;
using RofoServer.Domain.IdentityObjects;
using RofoServer.Domain.IRepositories;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RofoServer.Core.Logic.Authentication
{
    public class AuthenticateHandler : IRequestHandler<AuthenticationCommand, AuthenticateResponseModel>
    {
        private readonly IRepositoryManager _repo;
        private readonly IJwtServices _tokenService;
        protected IConfiguration _config;
        private User _user;

        public AuthenticateHandler(IRepositoryManager repo, IJwtServices tokenService, IConfiguration config) {
            _repo = repo;
            _tokenService = tokenService;
        }

        public async Task<AuthenticateResponseModel> Handle(AuthenticationCommand request, CancellationToken cancellationToken) {
            _user = await _repo.UserRepository.GetUserByEmail(request.Request.Email);

            if (!await validCredentials(request.Request.Password))
                return new AuthenticateResponseModel() { Errors = "INVALID_CREDENTIALS" };

            if (await isLockedOut())
                return new AuthenticateResponseModel() { Errors = "ACCOUNT_LOCKED" };

            if (!AccountConfirmed())
                return new AuthenticateResponseModel() { Errors = "EMAIL_CONFIRMATION_REQUIRED" };

            //if (TwoFactorEnabled() && !validTwoFactorCode(request.Request.AuthenticatorCode))
            //    return new AuthenticateResponseModel() { Errors = "INVALID_AUTHENTICATOR_CODE" };

            return await Authenticate();
        }


        private async Task<AuthenticateResponseModel> Authenticate()
        {
            await updateRefreshTokens();
            await _repo.Complete();
            return new AuthenticateResponseModel()
            {
                Id = _user.Id,
                Username = _user.UserName,
                Email = _user.Email,
                JwtToken = _tokenService.GenerateJwtToken(_user.UserClaims),
                RefreshToken = _user.RefreshTokens.Last().Token
            };
        }

        private async Task updateRefreshTokens()
        {
            _tokenService.RotateRefreshToken(_user.RefreshTokens);
            await _repo.UserRepository.UpdateAsync(_user);
        }


        private async Task<bool> validCredentials(string password) {
            var checkPassword = _repo.UserRepository.CheckUserPassword(_user, password);
            if (_user != null && !checkPassword)
                await manageLockouts();
            await _repo.Complete();
            return checkPassword;
        }

        private async Task manageLockouts() {
            var failedAttempts = await _repo.UserRepository.AccessFailedAsync(_user);
            if (failedAttempts >= int.Parse(_config["MaxFailedSignInAttempts"]))
                await _repo.UserRepository.SetLockoutAsync(_user, DateTime.Now.AddMinutes(int.Parse(_config["LockoutTime"])));
        }

        private bool TwoFactorEnabled()
            => _user.UserAuthDetails.TwoFactorEnabled;

        private bool AccountConfirmed()
            => _user.UserAuthDetails.AccountConfirmed;

        private async Task<bool> isLockedOut() {
            var locked = _user.UserAuthDetails.LockOutExpiry > DateTime.Now;
            if (!locked)
                await _repo.UserRepository.ResetAccessFailed(_user);
            await _repo.Complete();
            return locked;
        }

        private bool validTwoFactorCode(string code) {
            var t = new Totp(Convert.FromBase64String(code));
            return t.VerifyTotp(code, out var timeStep);
        }


    }
}
