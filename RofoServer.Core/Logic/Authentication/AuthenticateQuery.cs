using MediatR;
using RofoServer.Core.RequestModels;
using RofoServer.Core.ResponseModels;

namespace RofoServer.Core.Logic.Authentication
{
    public class AuthenticateQuery : IRequest<AuthenticateResponseModel>
    {
        public AuthenticateRequestModel Request { get; set; }

        public AuthenticateQuery(AuthenticateRequestModel myModel) {
            Request = myModel;
        }
    }
}
