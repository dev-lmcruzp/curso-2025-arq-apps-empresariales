namespace TSquad.Ecommerce.Infrastructure.Interface;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAllWithPaginationAsync(int pageNumber, int pageSize);
    Task<int> CountAsync();
    Task<T?> GetAsync(string customerId);
    Task<bool> InsertAsync(T customer);
    Task<bool> UpdateAsync(T entity);
    Task<bool> DeleteAsync(string customerId);
}