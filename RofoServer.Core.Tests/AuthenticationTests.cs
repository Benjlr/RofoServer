using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using RofoServer.Core.Logic;
using RofoServer.Core.Logic.Authentication;
using RofoServer.Core.RequestModels;
using RofoServer.Domain.IdentityObjects;
using RofoServer.Persistence;
using Xunit;
using Xunit.Sdk;

namespace RofoServer.Core.Tests
{
    public class AuthenticationTests
    {
        [Fact]
        public void ShouldAuthenticate() {
            var myUserManager = new Mock<RofoDbContext>();
            User t = new User()
            {
                Email = "test@email.com",
                PasswordHash = PasswordHasher.HashPassword("password"),
                UserName = "bob"
            };

            AuthenticateHandler handler = new AuthenticateHandler(new RofoRepository())

            AuthenticationHandler myHandler = new AuthenticationHandler(
                myUserManager.Object,
                new TokenServices(MockHelpers.TestConfiguration()));

            var result = myHandler.Handle(
                new IdentityServer.Core.UseCases.Authentication.AuthenticationCommand(new AuthenticateRequestModel()
                {
                    Email = "ben@test.com",
                    AuthenticatorCode = "",
                    Password = "password",
                }, "127.0.0.1"), new CancellationToken()).Result;

            Assert.Equal("ben@test.com", result.Email);
            Assert.Equal(new TokenServices(MockHelpers.TestConfiguration()).GenerateJwtToken("ben@test.com"), result.JwtToken);
            Assert.True(!string.IsNullOrWhiteSpace(result.RefreshToken));
        }

        //[Fact]
        //public void ShouldFailCredentials()
        //{
        //    var myUserManager = MockHelpers.MockUserManager<UbiquityUser>();
        //    var testUser = new UbiquityUser()
        //    {
        //        UserName = "Ben",
        //        Email = "ben@test.com",
        //    };
        //    testUser.PasswordHash = myUserManager.Object.PasswordHasher.HashPassword(testUser, "password");

        //    myUserManager.Setup(x => x.FindByEmailAsync(testUser.Email)).Returns(Task.FromResult(testUser));
        //    myUserManager.Setup(x => x.CheckPasswordAsync(testUser, "password")).Returns(Task.FromResult(true));
        //    myUserManager.Setup(x => x.VerifyTwoFactorTokenAsync(testUser, "Authenticator", "")).Returns(Task.FromResult(true));

        //    myUserManager.Object.CreateAsync(testUser).Wait();

        //    AuthenticationHandler myHandler = new AuthenticationHandler(
        //        myUserManager.Object,
        //        new TokenServices(MockHelpers.TestConfiguration()));

        //    var result = myHandler.Handle(
        //        new IdentityServer.Core.UseCases.Authentication.AuthenticationCommand(new AuthenticateRequestModel()
        //        {
        //            Email = "ben@test.com",
        //            AuthenticatorCode = "",
        //            Password = "wrong password",
        //        }, "127.0.0.1"), new CancellationToken()).Result;

        //    Assert.True(!string.IsNullOrWhiteSpace(result.Errors));
        //    Assert.Equal("INVALID_CREDENTIALS", result.Errors);

        //}

        //[Fact]
        //public void ShouldLockOut()
        //{
        //    var myUserManager = MockHelpers.MockUserManager<UbiquityUser>();
        //    var testUser = new UbiquityUser()
        //    {
        //        UserName = "Ben",
        //        Email = "ben@test.com",
        //    };
        //    testUser.PasswordHash = myUserManager.Object.PasswordHasher.HashPassword(testUser, "password");

        //    myUserManager.Setup(x => x.FindByEmailAsync(testUser.Email)).Returns(Task<>.FromResult(testUser));
        //    myUserManager.Setup(x => x.CheckPasswordAsync(testUser, "password")).Returns(Task.FromResult(true));
        //    myUserManager.Setup(x => x.IsLockedOutAsync(testUser)).Returns(Task.FromResult(true));

        //    myUserManager.Object.CreateAsync(testUser).Wait();

        //    AuthenticationHandler myHandler = new AuthenticationHandler(
        //        myUserManager.Object,
        //        new TokenServices(MockHelpers.TestConfiguration()));

        //    var tryFive = myHandler.Handle(
        //        new IdentityServer.Core.UseCases.Authentication.AuthenticationCommand(new AuthenticateRequestModel()
        //        {
        //            Email = "ben@test.com",
        //            AuthenticatorCode = "",
        //            Password = "password",
        //        }, "127.0.0.1"), new CancellationToken()).Result;
        //    Assert.Equal("ACCOUNT_LOCKED", tryFive.Errors);

        //}

        //[Fact]
        //public void ShouldFailTwoFactor()
        //{
        //    var myUserManager = MockHelpers.MockUserManager<UbiquityUser>();
        //    var testUser = new UbiquityUser()
        //    {
        //        UserName = "Ben",
        //        Email = "ben@test.com",
        //    };
        //    testUser.PasswordHash = myUserManager.Object.PasswordHasher.HashPassword(testUser, "password");

        //    myUserManager.Setup(x => x.FindByEmailAsync(testUser.Email)).Returns(Task.FromResult(testUser));
        //    myUserManager.Setup(x => x.CheckPasswordAsync(testUser, "password")).Returns(Task.FromResult(true));
        //    myUserManager.Setup(x => x.VerifyTwoFactorTokenAsync(testUser, "Authenticator", "")).Returns(Task.FromResult(true));

        //    myUserManager.Object.CreateAsync(testUser).Wait();

        //    AuthenticationHandler myHandler = new AuthenticationHandler(
        //        myUserManager.Object,
        //        new TokenServices(MockHelpers.TestConfiguration()));

        //    var result = myHandler.Handle(
        //        new IdentityServer.Core.UseCases.Authentication.AuthenticationCommand(new AuthenticateRequestModel()
        //        {
        //            Email = "ben@test.com",
        //            AuthenticatorCode = "2",
        //            Password = "password",
        //        }, "127.0.0.1"), new CancellationToken()).Result;

        //    Assert.True(!string.IsNullOrWhiteSpace(result.Errors));
        //    Assert.Equal("INVALID_AUTHENTICATOR_CODE", result.Errors);
        //}
    }
}
