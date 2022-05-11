using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RofoServer.Core;
using RofoServer.Core.Rofo.CommentRofo;
using RofoServer.Core.Rofo.GetAllComments;
using RofoServer.Core.Rofo.GetRofoImage;
using RofoServer.Core.Rofo.UploadRofo;
using RofoServer.Core.Rofo.ViewRofos;
using RofoServer.Core.Utils.TokenService;
using System.Net;
using System.Threading.Tasks;

namespace RofoServer.Controller;

public class RofoController : ApiController
{
    private readonly IMediator _mediator;

    public RofoController(IMediator mediator, IJwtServices jwt):base(jwt) {
        _mediator = mediator;
    }

    [HttpPost("uploadrofo")]
    [Authorize]
    [ProducesResponseType(typeof(UploadRofoResponseModel),(int)HttpStatusCode.OK)]
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
    [ProducesResponseType( typeof(ViewRofosResponseModel), (int) HttpStatusCode.OK)]
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
    [ProducesResponseType(typeof(GetImageResponseModel),(int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.RequestTimeout)]
    [ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetImage([FromQuery] GetImageRequestModel req) {
        if (ModelState.ErrorCount > 0)
            return BadRequest();

        req.Email = GetUserEmailClaim();

        var response = await _mediator.Send(new GetImageCommand(req));
        return Ok(response);
    }

    [HttpPost("comment")]
    [Authorize]
    [ProducesResponseType(typeof(CommentRofoResponseModel),(int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.RequestTimeout)]
    [ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Upload([FromBody] CommentRofoRequestModel req) {
        if (ModelState.ErrorCount > 0)
            return BadRequest();

        req.Email = GetUserEmailClaim();

        var response = await _mediator.Send(new CommentRofoCommand(req));
        return Ok(response);
    }

    [HttpGet("getcomments")]
    [Authorize]
    [ProducesResponseType(typeof(GetAllCommentsRofoResponseModel),(int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.RequestTimeout)]
    [ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetComments([FromQuery] GetAllCommentsRofoRequestModel req) {
        if (ModelState.ErrorCount > 0)
            return BadRequest();

        req.Email = GetUserEmailClaim();

        var response = await _mediator.Send(new GetAllCommentsRofoCommand(req));
        return Ok(response);
    }

}