using TSquad.Ecommerce.Domain.Entity;
using TSquad.Ecommerce.Domain.Interface;
using TSquad.Ecommerce.Infrastructure.Interface;

namespace TSquad.Ecommerce.Domain.Core;

public class CustomerDomain : ICustomerDomain
{
    private readonly IUnitOfWork _unitOfWork;

    public CustomerDomain(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await  _unitOfWork.Customers.GetAllAsync();
    }

    public async Task<IEnumerable<Customer>> GetAllWithPaginationAsync(int pageNumber, int pageSize)
    {
        return await _unitOfWork.Customers.GetAllWithPaginationAsync(pageNumber, pageSize);
    }

    public async Task<int> CountAsync()
    {
        return await  _unitOfWork.Customers.CountAsync();
    }

    public async Task<Customer?> GetAsync(string customerId)
    {
        return await _unitOfWork.Customers.GetAsync(customerId);
    }
    
    public async Task<bool> InsertAsync(Customer customer)
    {
        return await  _unitOfWork.Customers.InsertAsync(customer);
    }

    public async Task<bool> UpdateAsync(Customer customer)
    {
        return await  _unitOfWork.Customers.UpdateAsync(customer);
    }

    public async Task<bool> DeleteAsync(string customerId)
    {
        return await  _unitOfWork.Customers.DeleteAsync(customerId);
    }
}