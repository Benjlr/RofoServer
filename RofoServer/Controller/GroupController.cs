using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RofoServer.Core.Logic.CreateGroup;
using RofoServer.Core.ResponseModels.Ubiquity.IdentityServer.Core.Responses;
using RofoServer.Extensions;
using System.Net;
using System.Threading.Tasks;

namespace RofoServer.Controller
{
    public class GroupController : ApiController
    {
        private readonly IMediator _mediator;

        public GroupController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpPost("create-group")]
        [Authorize]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetail), (int) HttpStatusCode.RequestTimeout)]
        [ProducesResponseType(typeof(ErrorDetail), (int) HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroupRequestModel req) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrors());
            
            var response = await _mediator.Send(new CreateGroupCommand(req));
            return Ok(response);
        }

        //[HttpPost]
        //[Authorize]
        //[ProducesResponseType((int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.RequestTimeout)]
        //[ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.InternalServerError)]
        //public async Task<IActionResult> AddToGroup([FromBody] CreateGroupRequestModel req)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState.GetErrors());

        //    var response = await _mediator.Send(new CreateGroupCommand(req));
        //    return Ok(response);
        //}
    }
}
