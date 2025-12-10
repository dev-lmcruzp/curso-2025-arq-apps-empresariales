using FluentValidation.Results;

namespace TSquad.Ecommerce.CrossCutting.Common;

public class ResponseGeneric<T>
{
    public T? Data { get; set; }
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = null!;
    public IEnumerable<ValidationFailure> Errors { get; set; } = null!;
}