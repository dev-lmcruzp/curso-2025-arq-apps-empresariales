using TSquad.Ecommerce.Domain.Entity;

namespace TSquad.Ecommerce.Domain.Interface;

public interface ICustomerDomain
{
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<IEnumerable<Customer>> GetAllWithPaginationAsync(int pageNumber, int pageSize);
    Task<int> CountAsync();
    Task<Customer?> GetAsync(string customerId);
    Task<bool> InsertAsync(Customer customer);
    Task<bool> UpdateAsync(Customer customer);
    Task<bool> DeleteAsync(string customerId);
}