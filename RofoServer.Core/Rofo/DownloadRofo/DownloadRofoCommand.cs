using MediatR;

namespace RofoServer.Core.Rofo.DownloadRofo;

public class DownloadRofoCommand : IRequest<DownloadRofoResponseModel>
{
    public DownloadRofoRequestModel Request { get; set; }

    public DownloadRofoCommand(DownloadRofoRequestModel req) {
        Request = req;
    }

}