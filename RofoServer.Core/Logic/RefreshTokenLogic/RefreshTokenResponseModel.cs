﻿namespace RofoServer.Core.Logic.RefreshTokenLogic
{
    public class RefreshTokenResponseModel : ResponseBase
    {
        public string Email { get; set; }
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
    }
}