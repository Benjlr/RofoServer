using RofoServer.Domain.IdentityObjects;
using RofoServer.Domain.IRepositories;
using RofoServer.Domain.RofoObjects;
using System.Threading.Tasks;

namespace RofoServer.Persistence
{
    public class RofoGroupAccessRepository : Repository<RofoGroupAccess>, IRofoGroupAccessRepository
    {
        private RofoDbContext RofoContext => _cxt as RofoDbContext;
        public RofoGroupAccessRepository(RofoDbContext context) : base(context)
        { }
        
        public async Task<RofoGroupAccess> GetGroupPermission(User user, RofoGroup group) {
            return await RofoContext.GroupAccess.FindAsync(user.Id, group.Id);

        }

        public async Task AddOrUpdateGroupClaimAsync(RofoGroup group, User user, string rofoClaim) {
            var existing = await GetGroupPermission(user, group);
            existing.Rights = rofoClaim;
            await UpdateAsync(existing);
        }
    }
}
