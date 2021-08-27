using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RofoServer.Domain.IdentityObjects;
using RofoServer.Domain.RofoObjects;

namespace RofoServer.Domain
{
    public interface IRofoRepository
    {
        private List<Rofo> Rofos { get; set; }
        private List<User> Users { get; set; }
        private List<UserClaims> UserClaims { get; set; }
        private List<Claims> Claims { get; set; }

    }
}
