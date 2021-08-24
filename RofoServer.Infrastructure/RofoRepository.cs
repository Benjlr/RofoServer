using Microsoft.EntityFrameworkCore;
using RofoServer.Domain;
using RofoServer.Domain.IdentityObjects;
using RofoServer.Domain.RofoObjects;
using System.Threading.Tasks;

namespace RofoServer.Persistence
{
    public class RofoRepository:IRepository
    {
        public RofoRepository() {
        }
        public async Task<User> GetUserByEmail(string email) {
            await using var cxt = new RofoDbContext();
            return await cxt.Users.FirstOrDefaultAsync(x => x.Email.Equals(email));
        }

        public async Task CreateUser(User user) {
            await using var cxt = new RofoDbContext();
            await cxt.Users.AddAsync(user);
            await cxt.SaveChangesAsync();
        }

        public async Task AddRofo(Rofo rofo) {
            await using var cxt = new RofoDbContext();
            await cxt.Rofos.AddAsync(rofo);
            await cxt.SaveChangesAsync();
        }

        public async Task<Rofo> GetRofo(int id) {
            await using var cxt = new RofoDbContext();
            return await cxt.Rofos.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }
    }
}
