using RofoServer.Domain.IdentityObjects;
using RofoServer.Domain.RofoObjects;
using System.Threading.Tasks;

namespace RofoServer.Domain
{
    public interface IRofoManager
    {
        Task<User> GetUserByEmail(string email);
        Task CreateUser(User user);

        Task AddRofo(Rofo rofo);
        Task<Rofo> GetRofo(int id);

        


    }
}
