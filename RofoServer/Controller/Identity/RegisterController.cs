using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RofoServer.Core;
using RofoServer.Core.User.AccountConfirmation;
using RofoServer.Core.User.Register;
using RofoServer.Core.User.ValidateAccount;
using RofoServer.Extensions;
using System.Net;
using System.Threading.Tasks;

namespace RofoServer.Controller.Identity
{
    public class RegisterController : ApiController
    {
        private readonly IMediator _mediator;
        public RegisterController(IMediator mediator):base(null)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.RequestTimeout)]
        [ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Register([FromBody] RegisterRequestModel req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrors());

            return Ok(await _mediator.Send(new RegisterCommand(req)));
        }

        [HttpPost("request-email-confirmation")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.RequestTimeout)]
        [ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> SendConfirmationEmail([FromBody] AccountConfirmationEmailRequestModel req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrors());

            req.ConfirmationEndpoint = Url.Action(action: "ValidateAccount", controller: "Register", values: null, protocol: this.Request.Scheme);
            return Ok(await _mediator.Send(new AccountConfirmationEmailCommand(req)));
        }

        [HttpGet("confirm-account")]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.RequestTimeout)]
        [ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ValidateAccount([FromQuery] ValidateAccountRequestModel req)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrors());

            return Ok(await _mediator.Send(new ValidateAccountCommand(req)));
        }

        //Make 'we have sent you email page
        // make redirect to account confirm succeeded /failed page

        //[HttpPost("get-2fa-enablement-codes")]
        //[AllowAnonymous]
        //[ProducesResponseType((int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.RequestTimeout)]
        //[ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.InternalServerError)]
        //public async Task<IActionResult> GetTwoFactorSetupCode([FromBody] Get2FaEnableCodesRequestModel req)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState.GetErrors());

        //    return Ok(await _mediator.Send(new Get2FaEnableCodesCommand(req)));
        //}

        //[HttpPost("enable-2fa")]
        //[AllowAnonymous]
        //[ProducesResponseType((int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.RequestTimeout)]
        //[ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.InternalServerError)]
        //public async Task<IActionResult> EnableTwoFactorCode([FromBody] Enable2FaRequestModel req)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState.GetErrors());

        //    return Ok(await _mediator.Send(new Enable2FaCommand(req)));
        //}
    }
}
