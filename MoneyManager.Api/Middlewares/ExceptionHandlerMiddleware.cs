using Microsoft.AspNetCore.Diagnostics;
using Service.Exceptions;

namespace MoneyManager.Api.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionHandlerMiddleware> logger;
        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (CustomException exception)
            {
                context.Response.StatusCode = exception.Code;
                await context.Response.WriteAsJsonAsync(new
                {
                    Code = exception.Code,
                    Error = exception.Message
                });
            }
            catch (Exception exception)
            {
                this.logger.LogError($"{exception.ToString()}\n");
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new
                {
                    Code = 500,
                    Error = exception.Message,
                });
            }
        }
    }
}
