using System.Net;
using System.Text.Json;
using TSquad.Ecommerce.CrossCutting.Common;

namespace TSquad.Ecommerce.Services.WebApi.Modules.Middlewares;

/// <inheritdoc />
public class GlobalExceptionHandler : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }
    

    /// <inheritdoc />
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var message = ex.Message;
            _logger.LogError($"Exception details: {message}");
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new Response<object>()
            {
                Message =  message,
            };

            await JsonSerializer.SerializeAsync(context.Response.Body, response);
        }
    }
}