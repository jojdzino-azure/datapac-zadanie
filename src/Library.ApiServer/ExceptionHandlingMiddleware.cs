using FluentValidation;
using FluentValidation.Results;
using System.Text.Json;

namespace Library.ApiServer
{
    internal sealed class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) => _logger = logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                await HandleExceptionAsync(context, e);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var statusCode = 400;
            var response = new
            {
                title = GetTitle(exception),
                status = statusCode,
                detail = exception.Message,
                errors = GetErrors(exception)
            };
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }


        private static string GetTitle(Exception exception) =>
            exception switch
            {
                ApplicationException applicationException => applicationException.Message,
                _ => "Server Error"
            };

        private static IEnumerable<ValidationFailure> GetErrors(Exception exception)
        {
            IEnumerable<ValidationFailure> errors = null;
            if (exception is ValidationException validationException)
            {
                errors = validationException.Errors;
            }
            return errors;
        }
    }
}
