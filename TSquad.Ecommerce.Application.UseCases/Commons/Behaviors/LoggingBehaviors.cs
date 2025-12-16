using System.Text.Json;
using MediatR;
using TSquad.Ecommerce.CrossCutting.Logging;

namespace TSquad.Ecommerce.Application.UseCases.Commons.Behaviors;

public class LoggingBehaviors<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    // Se puede usar ILogger
    private readonly  IAppLogger<LoggingBehaviors<TRequest, TResponse>> _logger;

    public LoggingBehaviors(IAppLogger<LoggingBehaviors<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }


    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Request Handling - {typeof(TRequest).Name}", JsonSerializer.Serialize(request));
        var response = await next(cancellationToken);
        _logger.LogInformation($"Response Handling - {typeof(TRequest).Name}", JsonSerializer.Serialize(response));
        
        return response;
    }
}