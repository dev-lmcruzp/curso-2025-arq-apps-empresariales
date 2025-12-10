using TSquad.Ecommerce.Application.DTO;
using TSquad.Ecommerce.CrossCutting.Common;

namespace TSquad.Ecommerce.Application.Interface.UseCases;

public interface ICategoryApplication
{
    Task<Response<IEnumerable<CategoryDto>>> GetAllAsync();
}