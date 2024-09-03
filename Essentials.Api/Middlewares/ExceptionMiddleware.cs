using System.Net;
using Newtonsoft.Json;

namespace Essentials.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(
            HttpContext httpContext)
        {
            try
            {
                await _next(httpContext).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex).ConfigureAwait(false);
            }
        }

        private static async Task HandleExceptionAsync(
            HttpContext context,
            Exception exception)
        {
            try
            {              
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.OK;                              

                // file to write to.
                var message = $"Message: {exception.Message}: StackTrace: {exception.StackTrace}";
                File.WriteAllText("./log.txt", message);

                var result = new { Message = "Genel bir sorun oluştu. Lütfen daha sonra tekrar deneyiniz..." };
                await context.Response.WriteAsync(JsonConvert.SerializeObject(result)).ConfigureAwait(false);
                return;
            }
            catch
            {
                var message = $"Message: {exception.Message}: StackTrace: {exception.StackTrace}";
                File.WriteAllText("./log.txt", message);

                var result = new { Message = "Genel bir sorun oluştu. Lütfen daha sonra tekrar deneyiniz..." };
                await context.Response.WriteAsync(JsonConvert.SerializeObject(result)).ConfigureAwait(false);
                return;

            }
        }
    }
}
