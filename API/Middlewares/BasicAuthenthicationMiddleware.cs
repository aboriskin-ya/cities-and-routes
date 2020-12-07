using Microsoft.AspNetCore.Http;
using System;
using System.Text;
using System.Threading.Tasks;

namespace API.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class BasicAuthenthicationMiddleware
    {
        private readonly RequestDelegate _next;

        public BasicAuthenthicationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            string authHeader = httpContext.Request.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("Basic"))
            {
                string ecodeUsernameAndPassword = authHeader.Substring("Basic ".Length).Trim();
                Encoding encoding = Encoding.GetEncoding("UTF-8");
                string usernameAndPassword = encoding.GetString(Convert.FromBase64String(ecodeUsernameAndPassword));
                int index = usernameAndPassword.IndexOf(":");
                var username = usernameAndPassword.Substring(0, index);
                var password = usernameAndPassword.Substring(index + 1);
                if (username.Equals("Admin") && password.Equals("Cities_Pass"))
                {
                    await _next.Invoke(httpContext);
                }
                else
                {
                    SetUnauthorizedResponse(httpContext);
                    return;
                }
            }
            else
            {
                SetUnauthorizedResponse(httpContext);
                return;
            }
        }

        private static void SetUnauthorizedResponse(HttpContext httpContext)
        {
            httpContext.Response.StatusCode = 401;
            httpContext.Response.Headers.Add("WWW-Authenticate", @"Basic realm=""Access to the secured resource"", charset=""UTF-8""");
        }
    }
}
