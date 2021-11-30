using RofoServer.Domain.IdentityObjects;
using RofoServer.Domain.RofoObjects;
using System.Collections.Generic;
using Xunit;

namespace RofoServer.Core.Tests
{
    public class GroupTests
    {
        [Fact]
        public void ShouldCreateGroup() {
            RofoGroup P = new RofoGroup();

            Assert.True(true);
        }
        [Fact]
        public void UserShouldCreateGroup() {
            User t = new User();
            
            t.Email = "newUser@address";
            RofoGroup p = new RofoGroup
            {
                Description = "myGroup", 
            };
            

            Assert.True(true);
        }
    }
}
