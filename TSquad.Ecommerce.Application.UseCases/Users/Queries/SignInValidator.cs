using FluentValidation;

namespace TSquad.Ecommerce.Application.UseCases.Users.Queries;

public class SignInValidator : AbstractValidator<SignInQuery>
{
    public SignInValidator()
    {
        RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotNull().NotEmpty();
    }
}