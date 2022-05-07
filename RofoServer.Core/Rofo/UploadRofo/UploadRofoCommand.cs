using MediatR;

namespace RofoServer.Core.Rofo.UploadRofo;

public class UploadRofoCommand : IRequest<UploadRofoResponseModel>
{
    public UploadRofoRequestModel Request { get; set; }

    public UploadRofoCommand(UploadRofoRequestModel req) {
        Request = req;
    }

}