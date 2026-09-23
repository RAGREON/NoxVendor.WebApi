using System.Text.Json;

namespace NoxVendor.WebApi.Middlewares;

public class ExceptionHandler(
  RequestDelegate next,
  ILogger<ExceptionHandler> logger
)
{
  private readonly RequestDelegate _next = next;
  private readonly ILogger<ExceptionHandler> _logger = logger;

  public async Task Invoke(HttpContext context)
  {
    try
    {
      await _next(context);
    }
    catch (Exception e)
    {
      _logger.LogError("Something went wrong: {e}", e);
      await HandleException(context, e);
    }
  }

  private Task HandleException(HttpContext context, Exception e)
  {
    context.Response.ContentType = "application/json";
    context.Response.StatusCode = 500;

    var response = new {
      message = e.Message,
      statusCode = 500
    };

    return context.Response.WriteAsync(JsonSerializer.Serialize(response));
  }
}
