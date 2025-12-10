using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace TSquad.Ecommerce.Test.IntegrationTest;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(configurationBuilder =>
        {
            var internalBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
            
            configurationBuilder.AddConfiguration(internalBuilder);
        });
    }
}

// docker run -d --name redis-stack -e REDIS_ARGS="--requirepass Admin123." -p 6379:6379 -p 8001:8001 redis/redis-stack:latest