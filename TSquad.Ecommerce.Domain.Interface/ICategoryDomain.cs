using TSquad.Ecommerce.Domain.Entity;

namespace TSquad.Ecommerce.Domain.Interface;

public interface ICategoryDomain
{
    Task<IEnumerable<Category>> GetAllAsync();
}