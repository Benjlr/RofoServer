using MediatR;

namespace RofoServer.Core.Logic.AccountConfirmation
{
    public class AccountConfirmationEmailCommand : IRequest<AccountConfirmationEmailResponseModel>
    {
        public AccountConfirmationEmailRequestModel Request { get; set; }

        public AccountConfirmationEmailCommand(AccountConfirmationEmailRequestModel details) {
            Request = details;
        }
    }
}
