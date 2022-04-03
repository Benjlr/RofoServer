using MediatR;

namespace RofoServer.Core.Logic.ValidateAccount
{
    public class ValidateAccountCommand : IRequest<ValidateAccountResponseModel>
    {
        public ValidateAccountRequestModel Request { get; set; }

        public ValidateAccountCommand(ValidateAccountRequestModel req) {
            Request = req;
        }
    }
}
