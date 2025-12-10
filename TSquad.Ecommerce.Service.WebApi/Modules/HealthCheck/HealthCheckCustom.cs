using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace TSquad.Ecommerce.Service.WebApi.Modules.HealthCheck;

/// <inheritdoc />
public class HealthCheckCustom : IHealthCheck
{
    private readonly Random _random =  new Random();

    /// <inheritdoc />
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        var responseTime = _random.Next(1, 300);
        return responseTime switch
        {
            < 100 => Task.FromResult(HealthCheckResult.Healthy("Healthy result from HealthCheckCustom")),
            < 200 => Task.FromResult(HealthCheckResult.Degraded("Degraded result from HealthCheckCustom")),
            _ => Task.FromResult(HealthCheckResult.Unhealthy("Unhealthy result from HealthCheckCustom"))
        };
    }
}