using System;
using System.Collections.Generic;

namespace RofoServer.Core.Rofo.GetAllComments;

public class GetAllCommentsRofoResponseModel : ResponseBase
{
    public List<CommentResponse> Comments { get; set; }
}

public class CommentResponse
{
    public string UploadedByUserName { get; set; }

    public Guid ParentPhoto { get; set; }

    public DateTime UploadedDateTime { get; set; }

    public string Text { get; set; }

}
