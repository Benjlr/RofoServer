using Microsoft.EntityFrameworkCore;
using RofoServer.Domain.IdentityObjects;
using RofoServer.Domain.IRepositories;
using RofoServer.Domain.RofoObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RofoServer.Persistence
{
    public class RofoGroupRepository : Repository<RofoGroup>, IRofoGroupRepository
    {
        private RofoDbContext RofoContext => _cxt as RofoDbContext;
        public RofoGroupRepository(RofoDbContext context) : base(context)
        { }
        
        public  async Task<List<RofoGroup>> GetUsersGroups(RofoUser user) {
            var access = await RofoContext.GroupAccess.Where(x=>x.User.Id.Equals(user.Id)).Select(c=>c.Group.Id).ToListAsync();
            var groups = await RofoContext.Groups.Where(c => access.Any(v => v.Equals(c.Id))).ToListAsync();
            return groups;
        }

        public async Task<RofoGroup> GetGroupById(Guid groupId) {
            return await RofoContext.Groups.FirstOrDefaultAsync(x => x.SecurityStamp.Equals(groupId));
        }
    }
}
