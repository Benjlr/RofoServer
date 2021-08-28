using RofoServer.Domain.IRepositories;
using RofoServer.Domain.RofoObjects;

namespace RofoServer.Persistence
{
    public class RofoRepository : Repository<Rofo>, IRofoRepository
    {
        public RofoDbContext RofoContext => Cxt as RofoDbContext;
        public RofoRepository(RofoDbContext context) :base(context)
        { }
    }
}
