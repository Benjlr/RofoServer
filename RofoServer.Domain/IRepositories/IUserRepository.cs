using RofoServer.Domain.IdentityObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RofoServer.Domain.RofoObjects;

namespace RofoServer.Domain.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByRefreshTokenOrDefault(string refreshToken);
        bool CheckUserPassword(User user, string password);
        Task<int> AccessFailedAsync(User user);
        Task ResetAccessFailed(User user);
        Task SetLockoutAsync(User user, DateTime expiry);
        Task SetTwoFactorAsync(User user, bool enable);
        Task SetAccountConfirmedAsync(User user);

    }
}
