
using System;
using RofoServer.Domain.IdentityObjects;

namespace RofoServer.Domain.IRepositories
{

    public interface IDataTransaction : IDisposable
    {
        IRofoRepository Rofos { get; }
        User Users { get; }
        int Complete();

    }

}

