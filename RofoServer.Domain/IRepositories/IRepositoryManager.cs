using System;
using System.Threading.Tasks;

namespace RofoServer.Domain.IRepositories
{
    public interface IRepositoryManager : IDisposable
    {
        IRofoRepository RofoRepository { get; set; }
        IRofoGroupRepository RofoGroupRepository { get; set; }
        IUserRepository UserRepository { get; set; }

        Task<int> Complete();
    }
}
