using FluentValidation;
using TSquad.Ecommerce.Application.DTO;

namespace TSquad.Ecommerce.Application.Validator;

public class SignInValidator : AbstractValidator<SignInDto>
{
    public SignInValidator()
    {
        RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotNull().NotEmpty();
    }
}