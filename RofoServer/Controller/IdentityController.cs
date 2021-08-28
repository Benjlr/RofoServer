using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RofoServer.Core.Extensions;
using RofoServer.Core.Logic.Authentication;
using RofoServer.Core.RequestModels;
using RofoServer.Core.ResponseModels.Ubiquity.IdentityServer.Core.Responses;

namespace RofoServer.Controller
{
    public class IdentityController : ApiController
    {
        private readonly IMediator _mediator;
        
        public IdentityController(
            IMediator mediator) {
            _mediator = mediator;
        }


        [HttpPost]
        [AllowAnonymous]
        [Route(nameof(Login))]
        public async Task<IActionResult> Login(AuthenticateRequestModel model) {
            if (ModelState.ErrorCount > 0)
                return BadRequest();

            var response = await _mediator.Send(model, new CancellationToken());
            Response.SetCookie(new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            }, "response");
            return Ok(response);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route(nameof(Register))]
        public async Task<IActionResult> Register(RegisterRequestModel model) {
            if (ModelState.ErrorCount > 0)
                return BadRequest();

            var response = await _mediator.Send(model, new CancellationToken());
            return Ok(response);
        }

    }

    public class AuthenticationController : ApiController
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.RequestTimeout)]
        [ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateRequestModel req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.ErrorCount);

            //var response = await _mediator.Send(new AuthenticationCommand(req, this.HttpContext.GetIpAddress()));
            var response = await _mediator.Send(new AuthenticationCommand(req));

            Response.SetCookie(new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            }, response.RefreshToken);

            return Ok(response);
        }
    }
}
