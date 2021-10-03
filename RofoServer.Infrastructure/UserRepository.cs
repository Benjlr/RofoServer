using RofoServer.Core.Utils;
using RofoServer.Domain.IdentityObjects;
using RofoServer.Domain.IRepositories;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RofoServer.Persistence
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private RofoDbContext RofoContext => _cxt as RofoDbContext;

        public UserRepository(RofoDbContext context) : base(context) {
        }

        public async Task<User> GetUserByEmail(string email) =>
            await RofoContext.Users.FirstOrDefaultAsync(x => x.Email.Equals(email));

        public async Task<User> GetUserByRefreshTokenOrDefault(string refreshToken) =>
            await RofoContext.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == refreshToken)) ?? null;

        public bool CheckUserPassword(User user, string password) =>
            PasswordHasher.CheckPassword(user.PasswordHash, password);

        public async Task<int> AccessFailedAsync(User user) {
            user.UserAuthDetails.FailedLogInAttempts++;
            await UpdateAsync(user);
            return user.UserAuthDetails.FailedLogInAttempts;
        }

        public async Task ResetAccessFailed(User user) {
            user.UserAuthDetails.FailedLogInAttempts = 0;
            await UpdateAsync(user);
        }

        public async Task SetLockoutAsync(User user, DateTime expiry) {
            user.UserAuthDetails.LockOutExpiry = expiry;
            await UpdateAsync(user);
        }

        public async Task SetTwoFactorAsync(User user, bool enable) {
            user.UserAuthDetails.TwoFactorEnabled = enable;
            await UpdateAsync(user);
        }

        public async Task SetAccountConfirmedAsync(User user) {
            user.UserAuthDetails.AccountConfirmed = true;
            await UpdateAsync(user);
        }

    }
}
