namespace RofoServer.Core.User.Authentication;

public class AuthenticateResponseModel : ResponseBase
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string JwtToken { get; set; }
    public string RefreshToken { get; set; }
}