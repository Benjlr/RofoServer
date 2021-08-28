using System;
using System.Threading.Tasks;

namespace RofoServer.Domain.IRepositories
{

    public interface IDataTransaction : IDisposable
    {
        IRofoRepository Rofos { get; }
        IUserRepository Users { get; }
        Task<int> Complete();

    }

}

