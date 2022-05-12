using System;
using System.Collections.Generic;
using RofoServer.Core.Group.ViewGroups;
using RofoServer.Core.Rofo.GetAllComments;

namespace RofoServer.Core.Rofo.ViewRofos;

public class ViewRofosResponseModel : ResponseBase
{
    public List<RofoResponseModel> Rofos { get; set; }
}

public class RofoResponseModel
{
    public string Description { get; set; }
    public string PhotoUploadedByUserName { get; set; }
    public GroupResponse Group { get; set; }
    public List<CommentResponse> Comments { get; set; }
    public DateTime UploadedDate { get; set; }
    public Guid SecurityStamp { get; set; }
}

