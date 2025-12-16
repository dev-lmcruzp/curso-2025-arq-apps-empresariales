using TSquad.Ecommerce.CrossCutting.Common;

namespace TSquad.Ecommerce.Application.UseCases.Commons.Exceptions;

public class ValidationExceptionCustom : Exception
{
    public ValidationExceptionCustom() : base("One or more validation failures have occurred.")
        => Errors = new List<BaseError>();
    
    public ValidationExceptionCustom(IEnumerable<BaseError>? errors) : this()
        => Errors = errors;
    

    public IEnumerable<BaseError>? Errors { get; }
}