using TSquad.Ecommerce.Domain.Entities;

namespace TSquad.Ecommerce.Application.Interface.Persistence;

public interface IDiscountRepository : IGenericRepository<Discount>
{
    Task<List<Discount>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Discount?> GetAsync(int id, CancellationToken cancellationToken = default);
}