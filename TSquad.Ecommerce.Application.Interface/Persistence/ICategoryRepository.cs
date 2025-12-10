using TSquad.Ecommerce.Domain;
using TSquad.Ecommerce.Domain.Entities;

namespace TSquad.Ecommerce.Application.Interface.Persistence;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllAsync();
}