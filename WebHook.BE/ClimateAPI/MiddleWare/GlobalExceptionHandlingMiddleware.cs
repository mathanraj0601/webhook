
using Microsoft.AspNetCore.Mvc;
using System;

namespace ClimateAPI.MiddleWare
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                var result = new { code = 500, error = ex.Message };
                await context.Response.WriteAsJsonAsync(result);
                return;
            }
        }
    }
}
