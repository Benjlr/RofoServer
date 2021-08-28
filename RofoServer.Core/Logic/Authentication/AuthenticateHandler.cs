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
        private readonly IUserRepository _repo;
        private readonly ITokenServices _tokenService;
        protected IConfiguration _config;
        private User _user;

        public AuthenticateHandler(IUserRepository repo, ITokenServices tokenService, IConfiguration config) {
            _repo = repo;
            _tokenService = tokenService;
        }

        public async Task<AuthenticateResponseModel> Handle(AuthenticationCommand request, CancellationToken cancellationToken) {
            _user = await _repo.GetUserByEmail(request.Request.Email);

            if (!await validCredentials(request.Request.Password))
                return new AuthenticateResponseModel() { Errors = "INVALID_CREDENTIALS" };

            if (isLockedOut())
                return new AuthenticateResponseModel() { Errors = "ACCOUNT_LOCKED" };

            if (!AccountConfirmed())
                return new AuthenticateResponseModel() { Errors = "EMAIL_CONFIRMATION_REQUIRED" };

            if (TwoFactorEnabled() && !validTwoFactorCode(request.Request.AuthenticatorCode))
                return new AuthenticateResponseModel() { Errors = "INVALID_AUTHENTICATOR_CODE" };

            return await Authenticate();
        }


        private async Task<AuthenticateResponseModel> Authenticate()
        {
            await updateRefreshTokens();
            return new AuthenticateResponseModel()
            {
                Id = _user.Id,
                Email = _user.Email,
                JwtToken = _tokenService.GenerateJwtToken(_user.Email),
                RefreshToken = _user.UserAuthDetails.RefreshTokens.Last().Token
            };
        }

        private async Task updateRefreshTokens()
        {
            _tokenService.RotateRefreshToken(_user.UserAuthDetails.RefreshTokens);
            await _repo.UpdateAsync(_user);
        }


        private async Task<bool> validCredentials(string password)
        {
            var checkPassword = _repo.CheckUserPassword(_user, password);
            if (_user != null && !checkPassword)
                await manageLockouts();

            return checkPassword;
        }

        private async Task manageLockouts() {
            var failedAttempts = await _repo.AccessFailedAsync(_user);
            if (failedAttempts >= int.Parse(_config["MaxFailedSignInAttempts"]))
                await _repo.SetLockoutAsync(_user, DateTime.Now.AddMinutes(int.Parse(_config["LockoutTime"])));
        }

        private bool TwoFactorEnabled()
            => _user.UserAuthDetails.TwoFactorEnabled;

        private bool AccountConfirmed()
            => _user.UserAuthDetails.AccountConfirmed;

        private bool isLockedOut() {
            var locked = _user.UserAuthDetails.LockOutExpiry > DateTime.Now;
            if (!locked)
                _repo.ResetAccessFailed(_user);
            return locked;
        }

        private bool validTwoFactorCode(string code) {
            var t = new Totp(Convert.FromBase64String(code));
            return t.VerifyTotp(code, out var timeStep);
        }


    }
}
