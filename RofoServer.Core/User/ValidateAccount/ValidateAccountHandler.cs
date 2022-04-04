using MediatR;
using RofoServer.Core.Utils;
using RofoServer.Domain.IRepositories;
using System.Threading;
using System.Threading.Tasks;

namespace RofoServer.Core.User.ValidateAccount;

public class ValidateAccountHandler : IRequestHandler<ValidateAccountCommand, ValidateAccountResponseModel>
{
    private readonly IRepositoryManager _repository;
    private readonly ITokenGenerator _tokenGenerator;

    public ValidateAccountHandler(IRepositoryManager users,
        ITokenGenerator tokenGenerator) {
        _repository = users;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<ValidateAccountResponseModel> Handle(ValidateAccountCommand request, CancellationToken cancellationToken) {
        var user = await _repository.UserRepository.GetUserByEmail(request.Request.Email);
        if (user == null)
            return new ValidateAccountResponseModel() { Errors = "INVALID_REQUEST" };

        if (!await _tokenGenerator.ValidateAsync("ConfirmAccount", request.Request.ValidationCode, user))
            return new ValidateAccountResponseModel() { Errors = "ACCOUNT_CODE_INVALID" };

        await _repository.UserRepository.SetAccountConfirmedAsync(user);
        await _repository.Complete();
        return new ValidateAccountResponseModel();
    }
}
