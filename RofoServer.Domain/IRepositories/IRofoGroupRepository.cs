using System.Collections.Generic;
using System.Threading.Tasks;
using RofoServer.Domain.IdentityObjects;
using RofoServer.Domain.RofoObjects;

namespace RofoServer.Domain.IRepositories
{
    public interface IRofoGroupRepository : IRepository<RofoGroup>
    {
        Task<List<RofoGroup>> GetGroups(RofoUser user);

    }
}
