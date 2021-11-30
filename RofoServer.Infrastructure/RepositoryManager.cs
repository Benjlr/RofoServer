using RofoServer.Domain.IRepositories;
using System.Threading.Tasks;

namespace RofoServer.Persistence
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RofoDbContext _rofoContext;

        public RepositoryManager(RofoDbContext myContext) {
            _rofoContext = myContext;
            UserRepository = new UserRepository(myContext);
            RofoRepository = new RofoRepository(myContext);
            RofoGroupRepository = new RofoGroupRepository(myContext);
        }

        public IUserRepository UserRepository { get; set; }
        public IRofoRepository RofoRepository { get; set; }
        public IRofoGroupRepository RofoGroupRepository { get; set; }

        public async Task<int> Complete() => 
            await _rofoContext.SaveChangesAsync();
        
        public void Dispose()=>
            _rofoContext.Dispose();
        
    }
}
