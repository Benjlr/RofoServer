using MediatR;

namespace RofoServer.Core.Rofo.GetAllComments;

public class GetAllCommentsRofoCommand : IRequest<GetAllCommentsRofoResponseModel>
{
    public GetAllCommentsRofoRequestModel Request { get; set; }

    public GetAllCommentsRofoCommand(GetAllCommentsRofoRequestModel req) {
        Request = req;
    }

}