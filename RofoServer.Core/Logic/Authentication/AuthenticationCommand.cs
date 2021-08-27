using MediatR;
using RofoServer.Core.RequestModels;
using RofoServer.Core.ResponseModels;

namespace RofoServer.Core.Logic.Authentication
{
    public class AuthenticationCommand : IRequest<AuthenticateResponseModel>
    {
        public AuthenticateRequestModel Request { get; set; }
        public string IpAddress { get; set; }

        public AuthenticationCommand(AuthenticateRequestModel req, string ipAddress)
        {
            Request = req;
            IpAddress = ipAddress;
        }

    }
}
