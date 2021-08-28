using RofoServer.Core.Utils;
using RofoServer.Domain.IdentityObjects;
using RofoServer.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace RofoServer.Persistence
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private RofoDbContext RofoContext => _cxt as RofoDbContext;

        public UserRepository(RofoDbContext context) : base(context) {
        }

        public async Task<List<Claims>> GetUserClaims(User user) =>
            await RofoContext.UserClaims.Where(x => x.User.Id.Equals(user.Id)).Select(uc => uc.Claim).ToListAsync();

        public async Task<User> GetUserByEmail(string email) =>
            await RofoContext.Users.FirstOrDefaultAsync(x => x.Email.Equals(email));

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
