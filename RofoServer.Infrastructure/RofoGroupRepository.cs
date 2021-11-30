using RofoServer.Domain.IRepositories;
using RofoServer.Domain.RofoObjects;

namespace RofoServer.Persistence
{
    public class RofoGroupRepository : Repository<RofoGroup>, IRofoGroupRepository
    {
        private RofoDbContext RofoContext => _cxt as RofoDbContext;
        public RofoGroupRepository(RofoDbContext context) : base(context)
        { }


    }
}
