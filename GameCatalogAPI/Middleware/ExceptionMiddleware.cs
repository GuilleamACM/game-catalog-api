using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace GameCatalogAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate m_next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            m_next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await m_next(context);
            }
            catch
            {
                await HandleExceptionAsync(context);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context)
        {
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            await context.Response.WriteAsJsonAsync(new
                {Message = "An error occurred during your request, please try again."});
        }
    }
}