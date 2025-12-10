using TSquad.Ecommerce.Application.DTO;
using TSquad.Ecommerce.CrossCutting.Common;

namespace TSquad.Ecommerce.Application.Interface;

public interface ICustomerApplication
{
    
    Task<Response<IEnumerable<CustomerDto>>> GetAllAsync();
    Task<ResponsePagination<IEnumerable<CustomerDto>>> GetAllWithPaginationAsync(int pageNumber, int pageSize);
    Task<Response<CustomerDto>> GetAsync(string customerId);
    Task<Response<bool>> InsertAsync(CustomerDto customerDto);
    Task<Response<bool>> UpdateAsync(CustomerDto customerDto);
    Task<Response<bool>> DeleteAsync(string customerId);
}