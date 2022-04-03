using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace RofoServer.Controller
{
    public class StatusController : ApiController
    {
        public StatusController() {
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult Status() {
            return Ok("Alive");
        }

    }

}
