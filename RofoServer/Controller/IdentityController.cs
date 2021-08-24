using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RofoServer.Core.RequestModels;

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
}
