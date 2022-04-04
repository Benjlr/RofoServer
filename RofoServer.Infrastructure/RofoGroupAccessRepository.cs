using RofoServer.Domain.IdentityObjects;
using RofoServer.Domain.IRepositories;
using RofoServer.Domain.RofoObjects;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RofoServer.Persistence
{
    public class RofoGroupAccessRepository : Repository<RofoGroupAccess>, IRofoGroupAccessRepository
    {
        private RofoDbContext RofoContext => _cxt as RofoDbContext;
        public RofoGroupAccessRepository(RofoDbContext context) : base(context)
        { }
        
        public async Task<RofoGroupAccess> GetGroupPermission(RofoUser user, RofoGroup group) {
            return await RofoContext
                    .GroupAccess
                    .SingleOrDefaultAsync(x=> x.Group .Equals(group.Id) && x.User.Equals(user.Id));
        }

        public async Task AddOrUpdateGroupClaimAsync(RofoGroup group, RofoUser user, string rofoClaim) {
            var existing = await GetGroupPermission(user, group);
            existing.Rights = rofoClaim;
            await UpdateAsync(existing);
        }
    }
}
