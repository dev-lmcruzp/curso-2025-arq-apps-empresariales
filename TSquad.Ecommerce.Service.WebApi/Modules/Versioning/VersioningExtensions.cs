using Asp.Versioning;
using Asp.Versioning.ApiExplorer;

namespace TSquad.Ecommerce.Service.WebApi.Modules.Versioning;

/// <summary>
/// 
/// </summary>
public static class VersioningExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                // Por parametro
                //options.ApiVersionReader = new QueryStringApiVersionReader("api-version");
                
                // por cabecera
                // options.ApiVersionReader = new HeaderApiVersionReader("x-version");
                
                // Por path param
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV"; // Genera grupos v1, v2, etc.
                // Por path param
                options.SubstituteApiVersionInUrl = true;
            });
        
        services.AddEndpointsApiExplorer();
        return services;
    }
}