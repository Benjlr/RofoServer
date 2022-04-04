using System.Collections.Generic;
using System.Threading.Tasks;
using RofoServer.Domain.IdentityObjects;
using RofoServer.Domain.RofoObjects;

namespace RofoServer.Domain.IRepositories
{
    public interface IRofoGroupAccessRepository : IRepository<RofoGroupAccess>
    {
        Task<RofoGroupAccess> GetGroupPermission(RofoUser user, RofoGroup group);
        Task AddOrUpdateGroupClaimAsync(RofoGroup group, RofoUser user, string rofoClaim);

    }
}
