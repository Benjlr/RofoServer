using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace RofoServer.Controller
{
    public class StatusController : ApiController
    {
        public StatusController() {
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Status() {
            return Ok("Alive");
        }

    }

}
