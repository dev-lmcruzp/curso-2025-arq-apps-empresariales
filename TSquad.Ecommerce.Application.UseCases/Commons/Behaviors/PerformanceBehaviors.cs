using System.Diagnostics;
using System.Text.Json;
using MediatR;
using TSquad.Ecommerce.CrossCutting.Logging;

namespace TSquad.Ecommerce.Application.UseCases.Commons.Behaviors;

public class PerformanceBehaviors<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly Stopwatch _timer;
    private readonly IAppLogger<TRequest> _logger;

    public PerformanceBehaviors(IAppLogger<TRequest> logger)
    {
        _timer = new Stopwatch();
        _logger = logger;
    }


    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();
        var response = await next(cancellationToken);
        _timer.Stop();
        var elapsedMilliseconds = _timer.ElapsedMilliseconds;
        if (elapsedMilliseconds <= 10) return response;
        
        var requestName = typeof(TRequest).Name;
        _logger.LogInformation($"Long running Request: {requestName} ({elapsedMilliseconds} milliseconds) ", JsonSerializer.Serialize(request));
        return response;
    }
}