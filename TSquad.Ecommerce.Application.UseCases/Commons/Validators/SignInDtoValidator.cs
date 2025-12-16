using FluentValidation;
using TSquad.Ecommerce.Application.DTO;

namespace TSquad.Ecommerce.Application.UseCases.Commons.Validators;

public class SignInDtoValidator : AbstractValidator<SignInDto>
{
    public SignInDtoValidator()
    {
        RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotNull().NotEmpty();
    }
}