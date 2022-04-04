using RofoServer.Domain.IdentityObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RofoServer.Domain.RofoObjects;

namespace RofoServer.Domain.IRepositories
{
    public interface IUserRepository : IRepository<RofoUser>
    {
        Task<RofoUser> GetUserByEmail(string email);
        Task<RofoUser> GetUserByRefreshTokenOrDefault(string refreshToken);
        bool CheckUserPassword(RofoUser user, string password);
        Task<int> AccessFailedAsync(RofoUser user);
        Task ResetAccessFailed(RofoUser user);
        Task SetLockoutAsync(RofoUser user, DateTime expiry);
        Task SetTwoFactorAsync(RofoUser user, bool enable);
        Task SetAccountConfirmedAsync(RofoUser user);

    }
}
