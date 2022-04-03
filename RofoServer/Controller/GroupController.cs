using System.Linq;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RofoServer.Extensions;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Net.Http.Headers;
using RofoServer.Core.Logic.Group.CreateGroup;
using RofoServer.Core.Logic.Group.ViewGroups;
using RofoServer.Core.ResponseModels.Ubiquity.IdentityServer.Core.Responses;
using RofoServer.Core.Utils.TokenService;

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

        [HttpGet]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.RequestTimeout)]
        [ProducesResponseType(typeof(ErrorDetail), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAllGRoups()
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
    }
}
