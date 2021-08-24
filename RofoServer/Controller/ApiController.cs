using Microsoft.AspNetCore.Mvc;

namespace RofoServer.Controller
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public abstract class ApiController : ControllerBase
    {
    }
}
