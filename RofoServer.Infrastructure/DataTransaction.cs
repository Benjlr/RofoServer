using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RofoServer.Domain.IRepositories;

namespace RofoServer.Persistence
{
    public class DataTransaction : IDataTransaction
    {
        private readonly RofoDbContext _cxt;

        public void Dispose(RofoDbContext context) {
            Rofos = new RofoRepository(_cxt);
            Users = new UserRepository(_cxt);
        }

        public IRofoRepository Rofos { get; }
        public IUserRepository Users { get; }
        public Task<int> Complete() {
            _cxt.SaveChangesAsync();
        }

        public void Dispose()
        {
            _cxt.Dispose();
        }
    }
}
