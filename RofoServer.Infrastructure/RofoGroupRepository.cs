using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RofoServer.Domain.IdentityObjects;
using RofoServer.Domain.IRepositories;
using RofoServer.Domain.RofoObjects;

namespace RofoServer.Persistence
{
    public class RofoGroupRepository : Repository<RofoGroup>, IRofoGroupRepository
    {
        private RofoDbContext RofoContext => _cxt as RofoDbContext;
        public RofoGroupRepository(RofoDbContext context) : base(context)
        { }
        
        public  async Task<List<RofoGroup>> GetGroups(User user) {
            var access = await RofoContext.GroupAccess.Where(x=>x.User.Equals(user.Id)).ToListAsync();
            return await RofoContext.Groups.Where(c=>access.Any(v=>v.Group.Equals(c.Id))).ToListAsync();
        }

        public async Task<RofoGroupAccess> GetGroupPermission(User user, RofoGroup group) {
            return await RofoContext.GroupAccess.FindAsync(user, group);

        }

        public async Task AddOrUpdateGroupClaimAsync(RofoGroup group, User user, string rofoClaim) {
            var existing = await GetGroupPermission(user, group);
            existing.Rights = rofoClaim;

            await RofoContext.SaveChangesAsync();
        }
    }
}
