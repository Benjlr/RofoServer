using RofoServer.Domain.IRepositories;
using RofoServer.Domain.RofoObjects;

namespace RofoServer.Persistence
{
    public class RofoRepository : Repository<Rofo>, IRofoRepository
    {
        private RofoDbContext RofoContext => _cxt as RofoDbContext;
        public RofoRepository(RofoDbContext context) :base(context)
        { }


    }
}
