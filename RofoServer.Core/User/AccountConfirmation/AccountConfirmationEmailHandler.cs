using System;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using RofoServer.Core.User.ValidateAccount;
using RofoServer.Core.Utils;
using RofoServer.Core.Utils.Emailer;
using RofoServer.Domain.IRepositories;

namespace RofoServer.Core.User.AccountConfirmation;

public class AccountConfirmationEmailHandler : IRequestHandler<AccountConfirmationEmailCommand, AccountConfirmationEmailResponseModel>
{
    private readonly IRepositoryManager _repository;
    private readonly IEmailer _emailer;
    private readonly ITokenGenerator _tokenGenerator;
    private AccountConfirmationEmailRequestModel _req;
    private Domain.IdentityObjects.RofoUser _user;


    public AccountConfirmationEmailHandler(
        IRepositoryManager repository,
        IEmailer emailer,
        IConfiguration config,
        ITokenGenerator tokenGen) {
        _repository = repository;
        _emailer = emailer;
        _tokenGenerator = tokenGen;
    }

    public async Task<AccountConfirmationEmailResponseModel> Handle(AccountConfirmationEmailCommand request,
        CancellationToken cancellationToken) {
        _user = await _repository.UserRepository.GetUserByEmail(request.Request.Email);
        _req = request.Request;

        if (_user == null || _user.UserAuthDetails.AccountConfirmed || !_repository.UserRepository.CheckUserPassword(_user, _req.Password))
            return new AccountConfirmationEmailResponseModel() { Errors = "INVALID_REQUEST" };

        await _repository.Complete();
        return await generateResponse();
    }

    private async Task<AccountConfirmationEmailResponseModel> generateResponse() {
        var code = await generateCode();
        var url = buildUrl(code);
        await emailCode(url);

        return new AccountConfirmationEmailResponseModel();
    }

    private async Task emailCode(string url) {
        await _emailer.SendEmailAsync(_user.Email, "Confirm your Rofo Account!",
            $"<div>" +
            $"<a></a><br />" +
            $"<a>Please confirm your rofos account by</a><br />" +
            $"<a href='{url}'>clicking here</a>" +
            $"</div>");
    }

    private string buildUrl(string code) {
        return HtmlEncoder.Default.Encode(new UriBuilder(_req.ConfirmationEndpoint)
        {
            Query = ConvertObjectToQueryString.GetQueryString(new ValidateAccountRequestModel
                { CallbackUrl = _req.CallbackUrl, Email = _req.Email, ValidationCode = code })
        }.ToString());
    }

    private async Task<string> generateCode() {
        var code = await _tokenGenerator.GenerateTokenAsync(_user, "ConfirmAccount");
        //   code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        return code;
    }
}

