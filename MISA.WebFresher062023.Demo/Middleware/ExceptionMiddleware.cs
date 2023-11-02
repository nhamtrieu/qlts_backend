using Microsoft.AspNetCore.Http;
using MISA.WebFresher062023.Demo.Domain;

namespace MISA.WebFresher062023.Demo    
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            Console.WriteLine(exception);
            context.Response.ContentType = "application/json";

            switch (exception)
            {
                case NotFoundException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    await context.Response.WriteAsync(
                        text: new BaseException()
                        {
                            ErrorCode = ((NotFoundException)exception).ErrorCode,
                            UserMessage = exception.Message,
                            DevMessage = exception.Message,
                            MoreInfo = "https://openapi.misa.com.vn/errorcode/e001",
                            TraceId = context.TraceIdentifier
                        }.ToString() ?? string.Empty
                        );
                    break;

                case ConflictException:
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    await context.Response.WriteAsync(
                        text: new BaseException()
                        {
                            ErrorCode = ((ConflictException)exception).ErrorCode,
                            UserMessage = exception.Message,
                            DevMessage = exception.Message,
                            MoreInfo = "https://openapi.misa.com.vn/errorcode/e001",
                            TraceId = context.TraceIdentifier
                        }.ToString() ?? string.Empty
                        );
                    break;
                case InvalidPasswordException:
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync(

                        text: new BaseException()
                        {
                            ErrorCode = ((InvalidPasswordException)exception).ErrorCode,
                            UserMessage = exception.Message,
                            DevMessage = exception.Message,
                            MoreInfo = "https://openapi.misa.com.vn/errorcode/e001",
                            TraceId = context.TraceIdentifier
                        }.ToString() ?? string.Empty
                        );
                    break;
                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsync(
                        text: new BaseException()
                        {
                            ErrorCode = context.Response.StatusCode,
                            UserMessage = "Lỗi hệ thông",
                            DevMessage = exception.Message,
                            MoreInfo = "https://openapi.misa.com.vn/errorcode/e001",
                            TraceId = context.TraceIdentifier
                        }.ToString() ?? string.Empty
                        );
                    break;
            }
        }
    }
}
