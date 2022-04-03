using MediatR;

namespace RofoServer.Core.Logic.Authentication
{
    public class AuthenticationCommand : IRequest<AuthenticateResponseModel>
    {
        public AuthenticateRequestModel Request { get; set; }

        public AuthenticationCommand(AuthenticateRequestModel req)
        {
            Request = req;
        }

    }
}
