using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace geolocalizationApi.Middlewares
{
    /// <summary>
    /// Middleware that handles all the errors during a request and return a structured message with the status code 500.
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// The middleware execution within the pipeline.
        /// </summary>
        /// <param name="context">The Http context of the current request.</param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                var error = new ErrorHandler
                {
                    Message = e.Message,
                    Details = e.StackTrace
                };

                await context.Response.WriteAsync(JsonConvert.SerializeObject(error));
            }
        }
    }
}