using MediatR;

namespace RofoServer.Core.User.ValidateAccount
{
    public class ValidateAccountCommand : IRequest<ValidateAccountResponseModel>
    {
        public ValidateAccountRequestModel Request { get; set; }

        public ValidateAccountCommand(ValidateAccountRequestModel req) {
            Request = req;
        }
    }
}
