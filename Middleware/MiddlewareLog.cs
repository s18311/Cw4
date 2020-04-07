using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Cw4.Middleware
{
    public class MiddlewareLog
    {
        private readonly RequestDelegate _next;
        
        public MiddlewareLog(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            httpContext.Request.EnableBuffering();
            if(httpContext.Request != null)
            {
                string path = httpContext.Request.Path;
                string method = httpContext.Request.Method;
                string query = httpContext.Request.QueryString.ToString();
                string body = "";


                using(StreamReader sr = new StreamReader(httpContext.Request.Body, System.Text.Encoding.UTF8, true, 1024, true))
                {
                    body = await sr.ReadToEndAsync();
                    httpContext.Request.Body.Position = 0;
                }

                File.AppendAllText(Directory.GetCurrentDirectory() + "MiddlewareLog.txt",
                    "Date: " + DateTime.Now.ToString() + "\n"
                    + "Path: " + path + "\n"
                    + "Method: " + method + "\n"
                    + "Query: " + query + "\n"
                    + "Body: " + body + "\n");
            }
            await _next(httpContext);
        }
    }
}
