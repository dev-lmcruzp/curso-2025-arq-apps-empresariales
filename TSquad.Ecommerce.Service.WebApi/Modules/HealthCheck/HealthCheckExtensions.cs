namespace TSquad.Ecommerce.Service.WebApi.Modules.HealthCheck;

/// <summary>
/// 
/// </summary>
public static class HealthCheckExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddHealthCheck(this IServiceCollection services, IConfiguration configuration)
    {
       /* services.AddHealthChecks()
            .AddSqlServer(configuration.GetConnectionString("NorthwindConnection")!,
                tags: ["database"]);

        services.AddHealthChecksUI().AddInMemoryStorage();*/

       services.AddHealthChecks()
           .AddSqlServer(configuration.GetConnectionString("NorthwindConnection")!,
               tags: ["database"])
           .AddRedis(configuration.GetConnectionString("RedisConnection")!, tags: ["cache"]);
           //.AddCheck<HealthCheckCustom>("HealthCheckCustom", tags: ["custom"]);
        
        services.AddHealthChecksUI().AddInMemoryStorage();
        
        return services;
    }
}