using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Timeouts;

namespace TSquad.Ecommerce.Services.WebApi.Modules.Feature;

/// <summary>
/// 
/// </summary>
public static class FeatureExtensions
{
    /// <summary>
    /// 
    /// </summary>
    public static IServiceCollection AddFeature(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(PresentationConstant.MyPolicyCors,
                b =>
                {
                    b.WithOrigins(configuration["Config:OriginCors"]!) // Replace with your allowed origin(s)
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });

        services.AddControllers().AddJsonOptions(options =>
        {
            var enumConverter = new JsonStringEnumConverter();
            options.JsonSerializerOptions.Converters.Add(enumConverter);
        });

        services.AddRequestTimeouts(options =>
        {
            options.DefaultPolicy = new RequestTimeoutPolicy()
            {
                Timeout = TimeSpan.FromMilliseconds(1500)
            };

            options.AddPolicy(PresentationConstant.MyPolicyRequestTimeout,
                TimeSpan.FromMilliseconds(2000));
        });
        return services;
    }
}