using RofoServer.Core.Logic;
using RofoServer.Domain.IdentityObjects;
using RofoServer.Domain.IRepositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using RofoServer.Core.Utils;

namespace RofoServer.Persistence
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public RofoDbContext RofoContext => Cxt as RofoDbContext;
        public UserRepository(RofoDbContext context) :base(context)
        { }

        public Task<List<Claims>> GetUserClaims(User user) =>
            RofoContext.Claims.Where(x => x.Id.Equals(user.Id)).ToListAsync();
        

        public Task<User> GetUserByEmail(string email) =>
             RofoContext.Users.FirstOrDefaultAsync(x => x.Email.Equals(email));

        public bool CheckUserPassword(User user, string password) =>
            PasswordHasher.CheckPassword(user.PasswordHash, password);

    }
}
