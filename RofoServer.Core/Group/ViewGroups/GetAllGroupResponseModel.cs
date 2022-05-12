using System;
using System.Collections.Generic;

namespace RofoServer.Core.Group.ViewGroups;

public class GetAllGroupResponseModel : ResponseBase
{
    public List<GroupResponse> Groups { get; set; }
}

public class GroupResponse
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid SecurityStamp { get; set; }

}

