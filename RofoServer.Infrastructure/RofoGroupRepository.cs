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
        
        public  async Task<List<RofoGroup>> GetGroups(RofoUser user) {
            var access = await RofoContext.GroupAccess.Where(x=>x.User.Id.Equals(user.Id)).Select(c=>c.Group.Id).ToListAsync();
            var groups = await RofoContext.Groups.Where(c => access.Any(v => v.Equals(c.Id))).ToListAsync();
            return groups;
        }
    }
}
