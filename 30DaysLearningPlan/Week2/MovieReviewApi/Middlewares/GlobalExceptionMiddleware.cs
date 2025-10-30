using System.Net;
using System.Text.Json;

namespace MovieReviewApi.Middlewares
{
  public class GlobalExceptionMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
      _next = next;
      _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
      try
      {
        await _next(context); // Continue pipeline
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, $"❌ Unhandled Exception: {ex.Message}");
        await HandleGlobalExceptionAsync(context, ex);
      }
    }

    private static Task HandleGlobalExceptionAsync(HttpContext context, Exception exception)
    {
      context.Response.ContentType = "application/json";
      context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

      var errorResponse = new
      {
        status = "error",
        message = "An unexpected error occurred. Please try again later.",
        details = exception.Message // ❗ Remove this line in production for security
      };

      var json = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
      {
        WriteIndented = true
      });

      return context.Response.WriteAsync(json);
    }
  }
}