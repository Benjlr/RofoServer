using MediatR;

namespace RofoServer.Core.Rofo.CommentRofo;

public class CommentRofoCommand : IRequest<CommentRofoResponseModel>
{
    public CommentRofoRequestModel Request { get; set; }

    public CommentRofoCommand(CommentRofoRequestModel req) {
        Request = req;
    }

}