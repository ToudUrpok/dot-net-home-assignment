using BlogPlatform.Services.Exceptions;
using BlogPlatform.Dtos;
using System.Net;
using System.Net.Mime;

namespace BlogPlatform.WebApi.Middleware;

public class ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
{
    private readonly ILogger<ExceptionHandlerMiddleware> _logger = logger;
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "error during executing {Context}", context.Request.Path.Value);
            await HandleCustomExceptionResponseAsync(context, exception);
        }
    }

    private static async Task HandleCustomExceptionResponseAsync(HttpContext context, Exception exception)
    {
        var statusCode = exception switch
        {
            UserLoginException or
            UnauthorizedAccessException
                => HttpStatusCode.Unauthorized,
            EntityNotFoundException or
            KeyNotFoundException
                => HttpStatusCode.NotFound,
            EntityAlreadyExistsException
                => HttpStatusCode.Conflict,
            EntityDataValidationException or
            ArgumentException or
            InvalidOperationException
                => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.InternalServerError,
        };

        context.Response.ContentType = MediaTypeNames.Application.Json;
        context.Response.StatusCode = (int)statusCode;
        ErrorDto response = new ()
        {
            StatusCode = context.Response.StatusCode,
            Message = exception.Message,
            Details = exception.StackTrace?.ToString()
        };
        await context.Response.WriteAsJsonAsync(response);
    }
}
