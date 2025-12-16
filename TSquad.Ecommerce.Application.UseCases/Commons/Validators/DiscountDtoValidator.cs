using FluentValidation;
using TSquad.Ecommerce.Application.DTO;

namespace TSquad.Ecommerce.Application.UseCases.Commons.Validators;

public class DiscountDtoValidator : AbstractValidator<DiscountDto>
{
    public DiscountDtoValidator()
    {
        RuleFor(d => d.Name).NotNull().NotEmpty().WithMessage("Name is required");
        RuleFor(d => d.Description).NotNull().NotEmpty().WithMessage("Description is required");
        RuleFor(d => d.Percent).NotNull().NotEmpty().GreaterThan(0).WithMessage("Percent is required");
    }
}