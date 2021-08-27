using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RofoServer.Core.Extensions
{
    public static class HttpExtensions
    {
        public static void SetCookie(this HttpResponse resp, CookieOptions cookieOptions, string cookie) {
            resp.Cookies.Append("refreshToken", cookie, cookieOptions);
        }
    }
}
