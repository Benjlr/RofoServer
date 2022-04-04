﻿using System;
using System.Text.Encodings.Web;
using MediatR;
using Microsoft.Extensions.Configuration;
using RofoServer.Core.Group.CreateGroup;
using RofoServer.Core.Utils;
using RofoServer.Domain.IRepositories;
using System.Threading;
using System.Threading.Tasks;
using RofoServer.Core.User.ValidateAccount;
using RofoServer.Core.Utils.Emailer;
using RofoServer.Domain.IdentityObjects;
using RofoServer.Domain.RofoObjects;

namespace RofoServer.Core.Group.AddToGroup;

public class InviteToGroupHandler : IRequestHandler<InviteToGroupCommand, InviteToGroupGroupResponseModel>
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

    public async Task<InviteToGroupGroupResponseModel> Handle(InviteToGroupCommand request, CancellationToken cancellationToken) {
        _req = request.Request;
        _user = await _repo.UserRepository.GetUserByEmail(_req.Email);
        if (_user == null)
            return new InviteToGroupGroupResponseModel() { Errors = "INVALID USER" };

        _group = await _repo.RofoGroupRepository.SingleOrDefaultAsync(x => x.Id.Equals(_req.GroupId));
        if(_group == null)
            return new InviteToGroupGroupResponseModel() { Errors = "INVALID REQUEST" };

        var userRights = await _repo.RofoGroupAccessRepository.GetGroupPermission(_user, _group);
        if(userRights.Rights != RofoClaims.READ_WRITE_GROUP_CLAIM)
            return new InviteToGroupGroupResponseModel() { Errors = "INVALID REQUEST" };
        
        await _repo.Complete();



        return await generateResponse();
    }

    private async Task<CreateGroupResponseModel> generateResponse()
    {
        var code = await generateCode();
        var url = buildUrl(code);
        await emailCode(url);

        return new CreateGroupResponseModel();
    }

    private async Task emailCode(string url) {
        await _emailer.SendEmailAsync(_req.NewMemberEmail, "You have been invited to a Group!",
            $"<div>" +
            $"<a></a><br />" +
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