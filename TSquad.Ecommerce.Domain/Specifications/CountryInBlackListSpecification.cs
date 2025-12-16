using TSquad.Ecommerce.Domain.Common;
using TSquad.Ecommerce.Domain.Entities;

namespace TSquad.Ecommerce.Domain.Specifications;

public class CountryInBlackListSpecification : ISpecification<Customer>
{
    private readonly List<string> _countriesInBlackList =
    [
        "Argentina",
        "Brazil",
        "Chile",
        "Colombia",
        "Mexico"
    ];
    public bool IsSatisfiedBy(Customer entity)
    {
        return !_countriesInBlackList.Contains(entity.Country!);
    }
}