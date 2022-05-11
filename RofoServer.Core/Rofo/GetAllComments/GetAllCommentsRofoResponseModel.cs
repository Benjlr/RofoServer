using System.Collections.Generic;
using RofoServer.Domain.RofoObjects;

namespace RofoServer.Core.Rofo.GetAllComments
{
    public class GetAllCommentsRofoResponseModel : ResponseBase
    {
        public List<RofoComment> Comments { get; set; }
    }
}
