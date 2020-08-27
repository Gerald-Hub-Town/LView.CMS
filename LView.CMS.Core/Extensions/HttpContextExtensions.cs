using Microsoft.AspNetCore.Http;
using System.Linq;

namespace LView.CMS.Core.Extensions
{
    public static class HttpContextExtensions
    {
        public static string GetClientUserIp(this HttpContext context)
        {
            var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
                ip = context.Connection.RemoteIpAddress.ToString();
            return ip;
        }
    }
}
