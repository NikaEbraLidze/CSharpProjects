
using System.Net;

using MyApi.Models;

namespace MyApi.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            CommonResponse apiResponse = new();

            switch (ex)
            {
                case ArgumentException:
                    apiResponse.Message = ex.Message;
                    apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    apiResponse.Success = false;
                    break;
                case Exception:
                    apiResponse.Message = ex.Message;
                    apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                    apiResponse.Success = false;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = Convert.ToInt32(apiResponse.StatusCode);

            return context.Response.WriteAsJsonAsync(apiResponse);
        }
    }
}
