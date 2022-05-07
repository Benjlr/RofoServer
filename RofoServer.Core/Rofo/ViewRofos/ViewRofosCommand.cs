using MediatR;

namespace RofoServer.Core.Rofo.ViewRofos;

public class ViewRofosCommand : IRequest<ViewRofosResponseModel>
{
    public ViewRofosRequestModel Request { get; set; }

    public ViewRofosCommand(ViewRofosRequestModel req) {
        Request = req;
    }

}