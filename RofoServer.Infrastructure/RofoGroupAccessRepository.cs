using System;
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
                    .SingleOrDefaultAsync(x=> x.Group.Id .Equals(group.Id) && x.User.Id.Equals(user.Id));
        }

        public async Task<RofoGroupAccess> GetGroupPermission(RofoUser user, Guid group) {
            return await RofoContext
                .GroupAccess
                .SingleOrDefaultAsync(x => x.Group.SecurityStamp.Equals(group) && x.User.Id.Equals(user.Id));
        }

        public async Task AddOrUpdateGroupClaimAsync(RofoGroup group, RofoUser user, string rofoClaim) {
            var existing = await GetGroupPermission(user, group);
            if (existing != null) {
                existing.Rights = rofoClaim;
                await UpdateAsync(existing);
            }
            else {
                existing = new RofoGroupAccess()
                {
                    Group = group,
                    Rights = rofoClaim,
                    User = user
                };
                await AddAsync(existing);
            }

        }
    }
}
