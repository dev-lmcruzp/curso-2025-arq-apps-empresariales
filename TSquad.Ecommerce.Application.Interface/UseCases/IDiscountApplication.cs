using TSquad.Ecommerce.Application.DTO;
using TSquad.Ecommerce.CrossCutting.Common;

namespace TSquad.Ecommerce.Application.Interface.UseCases;

public interface IDiscountApplication
{

    Task<ResponsePagination<List<DiscountDto>>> GetAllWithPaginationAsync(int pageNumber, int pageSize);
    Task<Response<List<DiscountDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Response<DiscountDto?>> GetAsync(int id, CancellationToken cancellationToken = default);
    Task<Response<bool>> InsertAsync(DiscountDto discountDto, CancellationToken cancellationToken = default);
    Task<Response<bool>> UpdateAsync(DiscountDto discountDto, CancellationToken cancellationToken = default);
    Task<Response<bool>> DeleteAsync(int id, CancellationToken cancellationToken = default);
}