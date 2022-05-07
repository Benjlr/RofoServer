using System;
using System.Threading.Tasks;
using RofoServer.Domain.RofoObjects;

namespace RofoServer.Domain.IRepositories;

public interface IRofoRepository : IRepository<Rofo>
{
    Task<Rofo> GetByStamp(Guid stamp);
}
