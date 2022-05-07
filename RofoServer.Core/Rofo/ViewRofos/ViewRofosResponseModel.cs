using System.Collections.Generic;

namespace RofoServer.Core.Rofo.ViewRofos
{
    public class ViewRofosResponseModel : ResponseBase
    {
        public List<Domain.RofoObjects.Rofo> Rofos { get; set; }
    }
}
