using MediatR;

namespace RofoServer.Core.User.Register
{
    public class RegisterCommand : IRequest<RegisterResponseModel>
    {
        public RegisterRequestModel Request { get; set; }

        public RegisterCommand(RegisterRequestModel model) {
            Request = model;            
        }
    }
}
