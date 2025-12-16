namespace TSquad.Ecommerce.Services.WebApi.Modules.Middlewares;

/// <summary>
/// 
/// </summary>
public static class MiddlewareExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="appBuilder"></param>
    /// <returns></returns>
    public static IApplicationBuilder AddMiddleware(this IApplicationBuilder appBuilder)
    {
        return appBuilder.UseMiddleware<GlobalExceptionHandler>();
    }
}