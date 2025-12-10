using Microsoft.AspNetCore.RateLimiting;

namespace TSquad.Ecommerce.Service.WebApi.Modules.RateLimiter;

public static class RateLimiterExtensions
{
    public static IServiceCollection AddRateLimiter(this IServiceCollection services, IConfiguration configuration)
    {
        const string fixedWindowPolicy = "FixedWindowPolicy";
        services.AddRateLimiter(options =>
        {
            options.AddFixedWindowLimiter(policyName:fixedWindowPolicy, fixedWindow =>
            {
                fixedWindow.PermitLimit = int.Parse(configuration["RateLimiting:PermitLimit"]!);
                fixedWindow.Window = TimeSpan.FromSeconds(int.Parse(configuration["RateLimiting:Window"]!));
                fixedWindow.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
                fixedWindow.QueueLimit = int.Parse(configuration["RateLimiting:QueueLimit"]!);
            });
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
        });
        return services;
    }
}