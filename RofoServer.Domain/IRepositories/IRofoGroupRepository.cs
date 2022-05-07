using RofoServer.Domain.IdentityObjects;
using RofoServer.Domain.RofoObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RofoServer.Domain.IRepositories
{
    public interface IRofoGroupRepository : IRepository<RofoGroup>
    {
        Task<List<RofoGroup>> GetUsersGroups(RofoUser user);
        Task<RofoGroup> GetGroupById(Guid groupId);
    }
}
