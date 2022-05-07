using Microsoft.EntityFrameworkCore;
using RofoServer.Domain.IRepositories;
using RofoServer.Domain.RofoObjects;
using System;
using System.Threading.Tasks;

namespace RofoServer.Persistence;

public class RofoRepository : Repository<Rofo>, IRofoRepository
{
    private RofoDbContext RofoContext => _cxt as RofoDbContext;

    public RofoRepository(RofoDbContext context) : base(context) {
    }

    public async Task<Rofo> GetByStamp(Guid stamp) {
        return await RofoContext.Rofos.FirstOrDefaultAsync(x => x.SecurityStamp.Equals(stamp));
    }

}