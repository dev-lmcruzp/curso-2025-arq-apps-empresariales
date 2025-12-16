using FluentValidation;
using MediatR;
using TSquad.Ecommerce.Application.UseCases.Commons.Exceptions;
using TSquad.Ecommerce.CrossCutting.Common;

namespace TSquad.Ecommerce.Application.UseCases.Commons.Behaviors;

public class ValidationBehaviors<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviors(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any()) return await next(cancellationToken);
        
        var context = new ValidationContext<TRequest>(request);
        var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
        var failures = validationResults.Where(r => r.Errors.Count != 0).SelectMany(r => r.Errors)
            .Select(r => new BaseError()
            {
                PropertyMessage = r.PropertyName,
                ErrorMessage = r.ErrorMessage
            }).ToList();

        if (failures.Count != 0)
        {
            throw new ValidationExceptionCustom(failures);
        }

        return await next(cancellationToken);
    }
}