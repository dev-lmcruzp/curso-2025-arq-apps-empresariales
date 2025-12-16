using FluentValidation;

namespace TSquad.Ecommerce.Application.UseCases.Customers.Commands.CreateCustomerCommand;

public class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty().NotEmpty().MinimumLength(5);
    }
}