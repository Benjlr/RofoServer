using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using RofoServer.Core.ResponseModels.Ubiquity.IdentityServer.Core.Responses;
using RofoServer.Core.Utils.TokenService;
using RofoServer.Extensions;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using RofoServer.Core.Group.AddToGroup;
using RofoServer.Core.Group.CreateGroup;
using RofoServer.Core.Group.ViewGroups;

namespace RofoServer.Controller
{
    public class GroupController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly IJwtServices _jwtService;

        public GroupController(IMediator mediator, IJwtServices jwtReader) {
            _mediator = mediator;
            _jwtService = jwtReader;
        }

        [HttpPost("create")]
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

        [HttpGet("get")]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.RequestTimeout)]
        [ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllGroups()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrors());

            var accessToken = Request.Headers[HeaderNames.Authorization];
            var response = await _mediator.Send(new GetAllGroupsRequestModel()
            {
                Email = _jwtService.GetClaimsFromToken(accessToken).FirstOrDefault(x=>x.Type.Equals("email"))?.Value ?? ""
            });
            return Ok(response);
        }

        [HttpPost("invite")]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.RequestTimeout)]
        [ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> InviteToGroup([FromBody] AddToGroupRequestModel request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrors());

            var accessToken = Request.Headers[HeaderNames.Authorization];
            var response = await _mediator.Send(new addtogr()
            {
                Email = _jwtService.GetClaimsFromToken(accessToken).FirstOrDefault(x => x.Type.Equals("email"))?.Value ?? ""
            });
            return Ok(response);
        }
    }
}
