using RofoServer.Domain.IdentityObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RofoServer.Domain.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<List<Claims>> GetUserClaims(User user);
        Task<User> GetUserByEmail(string email);
        bool CheckUserPassword(User user, string password);

    }
}
