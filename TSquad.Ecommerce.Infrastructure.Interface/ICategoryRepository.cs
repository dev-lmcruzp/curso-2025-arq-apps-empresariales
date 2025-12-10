using TSquad.Ecommerce.Domain.Entity;

namespace TSquad.Ecommerce.Infrastructure.Interface;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllAsync();
}