using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RofoServer.Core;
using RofoServer.Core.User.RefreshTokenLogic;
using RofoServer.Core.User.RevokeToken;
using RofoServer.Extensions;

namespace RofoServer.Controller.Identity;

public class TokenController : ApiController
{
    private readonly IMediator _mediator;

    public TokenController(IMediator mediator):base(null) {
        _mediator = mediator;
    }

    [HttpPost("refresh-token")]
    //[Authorize]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.RequestTimeout)]
    [ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> RefreshUserToken([FromBody] RefreshTokenRequestModel req) {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrors());

        var response = await _mediator.Send(new RefreshTokenCommand(req));
        return Ok(response);
    }


    [HttpPost("revoke-token")]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.RequestTimeout)]
    [ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> RevokeToken([FromBody] RevokeRefreshTokenRequestModel req) {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrors());

        var response = await _mediator.Send(new RevokeRefreshTokenCommand(req));
        return Ok(response);
    }

}