﻿using System.Text.Json;

namespace RofoServer.Core.ResponseModels
{

    namespace Ubiquity.IdentityServer.Core.Responses
    {
        public class ErrorDetail
        {
            public int StatusCode { get; set; }
            public string Message { get; set; }
            public override string ToString()
            {
                return JsonSerializer.Serialize(this);
            }
        }
    }

}