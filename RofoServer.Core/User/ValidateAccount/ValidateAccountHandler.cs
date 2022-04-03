using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RofoServer.Core.Utils;
using RofoServer.Domain.IRepositories;

namespace RofoServer.Core.User.ValidateAccount
{
    public class ValidateAccountHandler : IRequestHandler<ValidateAccountCommand, ValidateAccountResponseModel>
    {
        private readonly IRepositoryManager _repository;

        public ValidateAccountHandler(IRepositoryManager users) {
            _repository = users;
        }

        public async Task<ValidateAccountResponseModel> Handle(ValidateAccountCommand request, CancellationToken cancellationToken) {
            var user = await _repository.UserRepository.GetUserByEmail(request.Request.Email);
            if (user == null)
                return new ValidateAccountResponseModel() { Errors = "INVALID_REQUEST" };

            if(!await new BasicTokenGenerator().ValidateAsync("ConfirmAccount", request.Request.ValidationCode, user))
                return new ValidateAccountResponseModel(){Errors = "ACCOUNT_CODE_INVALID"};

            await _repository.UserRepository.SetAccountConfirmedAsync(user);
            await _repository.Complete();
            return new ValidateAccountResponseModel();
        }
    }
}
