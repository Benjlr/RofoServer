using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using RofoServer.Core;
using RofoServer.Core.User.Authentication;
using RofoServer.Extensions;

namespace RofoServer.Controller.Identity;

public class AuthenticationController : ApiController
{
    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator) :base(null){
        _mediator = mediator;
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.RequestTimeout)]
    [ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(AuthenticateResponseModel), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequestModel req) {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrors());

        var response = await _mediator.Send(new AuthenticationCommand(req));
        return Ok(response);
    }
}