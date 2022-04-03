using System.Collections.Generic;
using RofoServer.Domain.RofoObjects;

namespace RofoServer.Core.Logic.Group.ViewGroups
{
    public class GetAllGroupResponseModel : ResponseBase
    {
        public List<RofoGroup> Groups { get; set; }
    }
}
