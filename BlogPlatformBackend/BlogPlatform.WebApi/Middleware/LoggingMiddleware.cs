using Microsoft.Net.Http.Headers;
using System.Text;

namespace BlogPlatform.WebApi.Middleware;

public class LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<LoggingMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        bool isPostMethod = context.Request.Method.Equals(HttpMethods.Post, StringComparison.CurrentCultureIgnoreCase);
        bool isAuthorizationRequest = isPostMethod
            && context.Request.Path.HasValue
            && context.Request.Path.Value.Equals("/Authorization", StringComparison.CurrentCultureIgnoreCase);
        bool isCreateUserRequest = isPostMethod
            && context.Request.Path.HasValue
            && context.Request.Path.Value.Equals("/User", StringComparison.CurrentCultureIgnoreCase);
        // to prevent user secret data from request/response body to be logged

        await LogRequest(context, !(isAuthorizationRequest || isCreateUserRequest));

        if (!isAuthorizationRequest)
        {
            var originalResponseBody = context.Response.Body;
            using var responseBody = new MemoryStream();
                context.Response.Body = responseBody;
                await _next.Invoke(context);
                await LogResponse(context, responseBody, originalResponseBody);
        }
        else
        {
            await _next.Invoke(context);
            await LogResponse(context);
        }
    }

    private async Task LogResponse(HttpContext context, MemoryStream? responseBody = null, Stream? originalResponseBody = null)
    {
        var responseContent = new StringBuilder();
        responseContent.AppendLine("=== Response Info ===");

        responseContent.AppendLine("-- headers");
        foreach (var (name, value) in context.Response.Headers)
        {
            responseContent.AppendLine($"header = {name}    value = {value}");
        }
        
        if (responseBody is not null && originalResponseBody is not null)
        {
            responseContent.AppendLine("-- body");
            responseBody.Position = 0;
            var content = await new StreamReader(responseBody).ReadToEndAsync();
            responseContent.AppendLine($"body = {content}");
            responseBody.Position = 0;
            await responseBody.CopyToAsync(originalResponseBody);
            context.Response.Body = originalResponseBody;
        }

        _logger.LogInformation(responseContent.ToString());
    }

    private async Task LogRequest(HttpContext context, bool logBody)
    {
        var requestContent = new StringBuilder();

        requestContent.AppendLine("=== Request Info ===");
        requestContent.AppendLine($"method = {context.Request.Method.ToUpper()}");
        requestContent.AppendLine($"path = {context.Request.Path}");

        requestContent.AppendLine("-- headers");
        foreach (var (name, value) in context.Request.Headers)
        {
            string loggedValue = name.Equals(HeaderNames.Authorization, StringComparison.CurrentCultureIgnoreCase) ? string.Empty : value.ToString();
            requestContent.AppendLine($"header = {name}    value = {loggedValue}");
        }

        if (logBody)
        {
            requestContent.AppendLine("-- body");
            context.Request.EnableBuffering();
            var requestReader = new StreamReader(context.Request.Body);
            var content = await requestReader.ReadToEndAsync();
            requestContent.AppendLine($"body = {content}");
            context.Request.Body.Position = 0;
        }

        _logger.LogInformation(requestContent.ToString());
    }
}
