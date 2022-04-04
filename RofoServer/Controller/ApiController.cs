using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using RofoServer.Core.Utils.TokenService;

namespace RofoServer.Controller;
    //[Authorize]
[ApiController]
[Route("[controller]")]
public abstract class ApiController : ControllerBase
{
    protected IJwtServices _jwtService { get; set; }
    protected ApiController(IJwtServices jwt) {
        _jwtService = jwt;
    }
    public string GetUserEmailClaim() {
        var accessToken = Request.Headers[HeaderNames.Authorization];
        return _jwtService.GetClaimsFromToken(accessToken).FirstOrDefault(x => x.Type.Equals("email"))?.Value ?? "";
    }
}
