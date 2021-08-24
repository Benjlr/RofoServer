using System.Text.Json.Serialization;

namespace RofoServer.Core.ResponseModels
{
    public class AuthenticateResponseModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string JwtToken { get; set; }
        public string Errors { get; set; }

        [JsonIgnore] 
        public string RefreshToken { get; set; }
    }
}
