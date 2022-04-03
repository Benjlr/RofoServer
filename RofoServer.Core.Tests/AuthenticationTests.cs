using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using RofoServer.Core.Utils;
using RofoServer.Core.Utils.TokenService;
using RofoServer.Domain.IdentityObjects;
using RofoServer.Persistence;
using System.Threading;
using RofoServer.Core.User.Authentication;
using Xunit;

namespace RofoServer.Core.Tests
{
    public class AuthenticationTests
    {
        private RepositoryManager _userManager => new RepositoryManager(Utils.TestContext());
        User _user => new User()
        {
            Email = "test@email.com",
            PasswordHash = PasswordHasher.HashPassword("password"),
            UserName = "bob",
            
        };
        IConfiguration myConfig => Utils.TestConfiguration();


        [Fact]
        public void ShouldAuthenticate() {
            _userManager.UserRepository.AddAsync(_user).Wait();
            AuthenticateHandler handler = new AuthenticateHandler(_userManager, new JwtServices(myConfig), myConfig);
            var result = handler.Handle(
                new AuthenticationCommand(new AuthenticateRequestModel()
                {
                    Email = "ben@test.com",
                    Password = "password",
                    //AuthenticatorCode = ""
                }), new CancellationToken()).Result;

            Assert.Equal("ben@test.com", result.Email);
            Assert.Equal(new JwtServices(myConfig).GenerateJwtToken(new User()), result.JwtToken);
            Assert.True(!string.IsNullOrWhiteSpace(result.RefreshToken));
        }

        [Fact]
        public void ShouldFailCredentials() {
            _userManager.UserRepository.AddAsync(_user).Wait();
            AuthenticateHandler handler = new AuthenticateHandler(_userManager, new JwtServices(myConfig), myConfig);
            var result = handler.Handle(
                new AuthenticationCommand(new AuthenticateRequestModel()
                {
                    Email = "ben@test.com",
                    Password = "wrongpassword",
                    //AuthenticatorCode = ""
                }), new CancellationToken()).Result;
            
            Assert.True(!string.IsNullOrWhiteSpace(result.Errors));
            Assert.True(string.IsNullOrWhiteSpace(result.JwtToken));
            Assert.Equal("INVALID_CREDENTIALS", result.Errors);
        }

        [Fact]
        public void ShouldLockOut()
        {
            _userManager.UserRepository.AddAsync(_user).Wait();
            AuthenticateHandler handler = new AuthenticateHandler(_userManager, new JwtServices(myConfig), myConfig);
            AuthenticateResponseModel result = new AuthenticateResponseModel();
            for (int i = 0; i < 10; i++) {
                result = handler.Handle(
                    new AuthenticationCommand(new AuthenticateRequestModel()
                    {
                        Email = "ben@test.com",
                        Password = "wrongpassword",
                        //AuthenticatorCode = ""
                    }), new CancellationToken()).Result;
            }

            Assert.Equal("ACCOUNT_LOCKED", result.Errors);
        }

        [Fact(Skip = "Need to implement two factor")]
        public void ShouldFailTwoFactor()
        {
            _userManager.UserRepository.AddAsync(_user).Wait();
            AuthenticateHandler handler = new AuthenticateHandler(_userManager, new JwtServices(myConfig), myConfig);
            var result = handler.Handle(
                new AuthenticationCommand(new AuthenticateRequestModel()
                {
                    Email = "ben@test.com",
                    Password = "wrongpassword",
                    //AuthenticatorCode = ""
                }), new CancellationToken()).Result;

            Assert.True(!string.IsNullOrWhiteSpace(result.Errors));
            Assert.Equal("INVALID_AUTHENTICATOR_CODE", result.Errors);
        }

        [Fact]
        public void ShouldNotAuthenticateAccountWithUnconfirmedEmail()
        {
            _userManager.UserRepository.AddAsync(_user).Wait();
            AuthenticateHandler handler = new AuthenticateHandler(_userManager, new JwtServices(myConfig), myConfig);
            var result = handler.Handle(
                new AuthenticationCommand(new AuthenticateRequestModel()
                {
                    Email = "ben@test.com",
                    Password = "wrongpassword",
                    //AuthenticatorCode = ""
                }), new CancellationToken()).Result;

            Assert.True(!string.IsNullOrWhiteSpace(result.Errors));
            Assert.Equal("EMAIL_CONFIRMATION_REQUIRED", result.Errors);
        }
    }
}
