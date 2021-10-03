using System;
using System.Linq;
using System.Web;

namespace RofoServer.Core.Utils
{
    public class ConvertObjectToQueryString
    {
        public static string GetQueryString(object obj) {
            var properties = from p in obj.GetType().GetProperties()
                where p.GetValue(obj, null) != null
                select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null)?.ToString());

            return String.Join("&", properties.ToArray());
        }
    }
}
