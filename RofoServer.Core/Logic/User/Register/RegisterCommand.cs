using MediatR;

namespace RofoServer.Core.Logic.Register
{
    public class RegisterCommand : IRequest<RegisterResponseModel>
    {
        public RegisterRequestModel Request { get; set; }

        public RegisterCommand(RegisterRequestModel model) {
            Request = model;            
        }
    }
}
