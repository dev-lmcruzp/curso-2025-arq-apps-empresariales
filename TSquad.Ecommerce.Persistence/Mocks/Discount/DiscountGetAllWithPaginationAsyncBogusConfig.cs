using Bogus;
using TSquad.Ecommerce.Domain.Enums;

namespace TSquad.Ecommerce.Persistence.Mocks.Discount;

public class DiscountGetAllWithPaginationAsyncBogusConfig : Faker<Domain.Entities.Discount>
{
    public DiscountGetAllWithPaginationAsyncBogusConfig()
    {
        // ReSharper disable once VirtualMemberCallInConstructor
        RuleFor(p => p.Id, f => f.IndexFaker + 1);
        // ReSharper disable once VirtualMemberCallInConstructor
        RuleFor(p => p.Name, f => f.Lorem.Word());
        // ReSharper disable once VirtualMemberCallInConstructor
        RuleFor(p => p.Description, f => f.Lorem.Word());
        // ReSharper disable once VirtualMemberCallInConstructor
        RuleFor(p => p.Percent, f => f.Random.Int(50, 100));
        // ReSharper disable once VirtualMemberCallInConstructor
        RuleFor(p => p.Status, f => f.PickRandom<DiscountStatus>());
    }
}