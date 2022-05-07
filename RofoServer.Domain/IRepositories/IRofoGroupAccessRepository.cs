using RofoServer.Domain.IdentityObjects;
using RofoServer.Domain.RofoObjects;
using System;
using System.Threading.Tasks;

namespace RofoServer.Domain.IRepositories
{
    public interface IRofoGroupAccessRepository : IRepository<RofoGroupAccess>
    {
        Task<RofoGroupAccess> GetGroupPermission(RofoUser user, RofoGroup group);
        Task<RofoGroupAccess> GetGroupPermission(RofoUser user, Guid group);
        Task AddOrUpdateGroupClaimAsync(RofoGroup group, RofoUser user, string rofoClaim);

    }
}
