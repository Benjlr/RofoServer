using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RofoServer.Domain.IdentityObjects;
using RofoServer.Domain.RofoObjects;

namespace RofoServer.Domain.IRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        Claims GetUserClaims(User user);
        RefreshToken GetUserRefreshToken(User user);


    }
}
