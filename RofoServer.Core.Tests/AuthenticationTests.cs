using System;
using System.Threading;
using RofoServer.Core.Logic.Authentication;
using RofoServer.Core.RequestModels;
using Xunit;
using Xunit.Sdk;

namespace RofoServer.Core.Tests
{
    public class AuthenticationTests
    {
        [Fact]
        public async void ShouldAuthenticateUser() {


            AuthenticateHandler myHandler = new AuthenticateHandler();
            AuthenticateRequestModel myReq = new AuthenticateRequestModel()
            {
                UserName = "benji",
                Password = "shhsecret"
            };

            var resolved = await myHandler.Handle(new AuthenticateQuery(myReq), new CancellationToken());


            Assert.Equal("benji", resolved.Username);
        }
    }
}
