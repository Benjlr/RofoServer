using MediatR;
using RofoServer.Core.User.ValidateAccount;
using RofoServer.Core.Utils;
using RofoServer.Core.Utils.Emailer;
using RofoServer.Domain.IdentityObjects;
using RofoServer.Domain.IRepositories;
using RofoServer.Domain.RofoObjects;
using System;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;

namespace RofoServer.Core.Group.AddToGroup;

public class InviteToGroupHandler : IRequestHandler<InviteToGroupCommand, InviteToGroupResponseModel>
{
    private readonly IRepositoryManager _repo;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IEmailer _emailer;
    private RofoUser _user;
    private RofoGroup _group;
    private InviteToGroupRequestModel _req;

    public InviteToGroupHandler(
        IRepositoryManager repo,
        ITokenGenerator tokenGenerator,
        IEmailer email) {
        _repo = repo;
        _tokenGenerator = tokenGenerator;
        _emailer = email;
    }

    public async Task<InviteToGroupResponseModel> Handle(InviteToGroupCommand request, CancellationToken cancellationToken) {
        _req = request.Request;
        _user = await _repo.UserRepository.GetUserByEmail(_req.Email);
        if (_user == null)
            return new InviteToGroupResponseModel() { Errors = "INVALID USER" };

        _group = await _repo.RofoGroupRepository.SingleOrDefaultAsync(x => x.Id.Equals(_req.GroupId));
        if(_group == null)
            return new InviteToGroupResponseModel() { Errors = "INVALID REQUEST" };

        var userRights = await _repo.RofoGroupAccessRepository.GetGroupPermission(_user, _group);
        if(userRights.Rights != RofoClaims.READ_WRITE_GROUP_CLAIM)
            return new InviteToGroupResponseModel() { Errors = "INVALID REQUEST" };
        
        await _repo.Complete();
        return await generateResponse();
    }

    private async Task<InviteToGroupResponseModel> generateResponse()
    {
        var code = await generateCode();
        var url = buildUrl(code);
        await emailCode(url);

        return new InviteToGroupResponseModel();
    }

    private async Task emailCode(string url) {
        await _emailer.SendEmailAsync(_req.NewMemberEmail, "You have been invited to a Group!",
            $"<div>" +
            $"<a></a><br />" +
            $"<a>You have been invited to a group! Lucky.</a><br />" +
            $"<a>To join, you must have a confirmed ROFO account</a><br />" +
            $"<a>If you don't, create one by</a><a href='{_req.RegisterEndpoint}'>clicking here</a><br />" +
            $"<a>Click the link below to join the {_group.Name} group by</a><br />" +
            $"<a href='{url}'>clicking here</a>" +
            $"</div>");
    }

    private string buildUrl(string code)
    {
        return HtmlEncoder.Default.Encode(new UriBuilder(_req.ConfirmationEndpoint)
        {
            Query = ConvertObjectToQueryString.GetQueryString(new ValidateAccountRequestModel
                { CallbackUrl = _req.CallbackUrl, Email = _req.Email, ValidationCode = code })
        }.ToString());
    }

    private async Task<string> generateCode() {
        var code = await _tokenGenerator.GenerateTokenAsync(_group.SecurityStamp, _req.AccessLevel, _req.NewMemberEmail);
        return code;
    }
}