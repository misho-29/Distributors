using Distributors.Core.Exceptions;
using System.Net;
using System.Security.Authentication;
using System.Text.Json;

namespace Distributors.Api.Middleware;

public class CustomExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            context.Response.ContentType = "application/json";
            var statusCode = exception switch
            {
                ValidationException => HttpStatusCode.BadRequest,
                ArgumentException => HttpStatusCode.BadRequest,
                NotFoundException => HttpStatusCode.NotFound,
                InvalidCredentialException => HttpStatusCode.Forbidden,
                UnauthorizedAccessException => HttpStatusCode.Forbidden,
                AuthenticationException => HttpStatusCode.Unauthorized,
                _ => HttpStatusCode.InternalServerError,
            };

            context.Response.StatusCode = (int)statusCode;

            if (context.Response.StatusCode == 500)
            {
                var logger = context.RequestServices.GetRequiredService<ILogger<CustomExceptionMiddleware>>();
                logger.LogError("Error: {Ex}", exception.ToString());
            }
            await context.Response
                .WriteAsync(JsonSerializer
                    .Serialize(new ErrorResponse
                    {
                        StatusCode = context.Response.StatusCode,
                        ErrorMessage = exception.Message,
                        Details = exception.Data["Details"]?.ToString(),
                    }));
        }
    }
}

public static class CustomExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionsMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomExceptionMiddleware>();
    }
}
