using System.Net;
using System.Text.Json;
using TSquad.Ecommerce.Application.UseCases.Commons.Exceptions;
using TSquad.Ecommerce.CrossCutting.Common;

namespace TSquad.Ecommerce.Services.WebApi.Modules.Middlewares;

/// <inheritdoc />
public class GlobalExceptionHandler : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
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
        catch (ValidationExceptionCustom ex)
        {
            context.Response.ContentType = "application/json";
            await JsonSerializer.SerializeAsync(context.Response.Body,
                new Response<object>
                {
                    Message = "Errores de validación",
                    Errors = ex.Errors ?? new List<BaseError>()
                });
        }
        catch (Exception ex)
        {
            var message = ex.Message;
            _logger.LogError($"Exception details: {message}");
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            await JsonSerializer.SerializeAsync(context.Response.Body,
                new Response<object>()
                {
                    Message = message,
                });
        }
    }
}