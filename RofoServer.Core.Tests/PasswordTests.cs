using RofoServer.Core.Logic;
using Xunit;

namespace RofoServer.Core.Tests
{
    public class PasswordTests
    {
        [Fact]
        public void ShouldGeneratePassword() {
            var hashed = PasswordHasher.HashPassword("test password");
            Assert.False(string.IsNullOrEmpty(hashed));
        }

        [Fact]
        public void ShouldVerifyPassword() {
            var hashed = PasswordHasher.HashPassword("test password");
            Assert.True(PasswordHasher.CheckPassword(hashed, "test password"));
        }

    }
}
