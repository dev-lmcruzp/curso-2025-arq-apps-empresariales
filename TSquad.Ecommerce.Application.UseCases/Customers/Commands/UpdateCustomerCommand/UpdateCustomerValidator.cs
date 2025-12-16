using FluentValidation;

namespace TSquad.Ecommerce.Application.UseCases.Customers.Commands.UpdateCustomerCommand;

public class UpdateCustomerValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty().NotEmpty().MinimumLength(5);
    }
}