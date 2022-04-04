using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RofoServer.Core;
using RofoServer.Core.Group.CreateGroup;
using RofoServer.Core.Group.ViewGroups;
using RofoServer.Core.Utils.TokenService;
using RofoServer.Extensions;
using System.Net;
using System.Threading.Tasks;
using RofoServer.Core.Group.AddToGroup;
using RofoServer.Core.Group.JoinGroup;

namespace RofoServer.Controller;

public class GroupController : ApiController
{
    private readonly IMediator _mediator;

    public GroupController(IMediator mediator, IJwtServices jwtReader) :base(jwtReader){
        _mediator = mediator;
    }

    [HttpPost("create")]
    [Authorize]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.RequestTimeout)]
    [ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> CreateGroup([FromBody] CreateGroupRequestModel req) {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrors());

        req.Email = GetUserEmailClaim();

        var response = await _mediator.Send(new CreateGroupCommand(req));
        return Ok(response);
    }

    [HttpGet("get")]
    [Authorize]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.RequestTimeout)]
    [ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetAllGroups() {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrors());

        var response = await _mediator.Send(new GetAllGroupsRequestModel
        {
            Email = GetUserEmailClaim()
        });
        return Ok(response);
    }

    [HttpPost("invite")]
    [Authorize]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.RequestTimeout)]
    [ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> InviteToGroup([FromBody] InviteToGroupRequestModel request) {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrors());

        request.Email = GetUserEmailClaim();
        request.ConfirmationEndpoint = Url.Action("JoinGroup", "Group", null, this.Request.Scheme);
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [HttpGet("join")]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.RequestTimeout)]
    [ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> JoinGroup([FromQuery] JoinGroupRequestModel request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrors());

        return Ok(await _mediator.Send(new JoinGroupCommand(request)));
    }
}