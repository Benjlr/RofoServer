using MediatR;

namespace RofoServer.Core.Rofo.GetRofoImage;

public class GetImageCommand : IRequest<GetImageResponseModel>
{
    public GetImageRequestModel Request { get; set; }

    public GetImageCommand(GetImageRequestModel req) {
        Request = req;
    }

}