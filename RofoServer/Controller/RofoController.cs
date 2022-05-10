using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RofoServer.Core;
using RofoServer.Core.Rofo.UploadRofo;
using RofoServer.Core.Rofo.ViewRofos;
using RofoServer.Core.Utils.TokenService;
using System.Net;
using System.Threading.Tasks;
using RofoServer.Core.Rofo.GetRofoImage;

namespace RofoServer.Controller;

public class RofoController : ApiController
{
    private readonly IMediator _mediator;

    public RofoController(IMediator mediator, IJwtServices jwt):base(jwt) {
        _mediator = mediator;
    }

    [HttpPost("uploadrofo")]
    [Authorize]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.RequestTimeout)]
    [ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Upload([FromBody]UploadRofoRequestModel req) {
        if (ModelState.ErrorCount > 0)
            return BadRequest();

        req.Email = GetUserEmailClaim();

        var response = await _mediator.Send(new UploadRofoCommand(req));
        return Ok(response);
    }


    [HttpGet("viewrofos")]
    [Authorize]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorDetail), (int) HttpStatusCode.RequestTimeout)]
    [ProducesResponseType(typeof(ErrorDetail), (int) HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> View([FromQuery] ViewRofosRequestModel req) {
        if (ModelState.ErrorCount > 0)
            return BadRequest();

        req.Email = GetUserEmailClaim();

        var response = await _mediator.Send(new ViewRofosCommand(req));
        return Ok(response);
    }


    [HttpGet("getrofoimage")]
    [Authorize]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.RequestTimeout)]
    [ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetImage([FromQuery] GetImageRequestModel req)
    {
        if (ModelState.ErrorCount > 0)
            return BadRequest();

        req.Email = GetUserEmailClaim();

        var response = await _mediator.Send(new GetImageCommand(req));
        return Ok(response);
    }


}