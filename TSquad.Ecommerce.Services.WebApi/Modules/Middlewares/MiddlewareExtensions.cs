namespace TSquad.Ecommerce.Services.WebApi.Modules.Middlewares;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder AddMiddleware(this IApplicationBuilder appBuilder)
    {
        return appBuilder.UseMiddleware<GlobalExceptionHandler>();
    }
}