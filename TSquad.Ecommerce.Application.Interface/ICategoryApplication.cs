using TSquad.Ecommerce.Application.DTO;
using TSquad.Ecommerce.CrossCutting.Common;

namespace TSquad.Ecommerce.Application.Interface;

public interface ICategoryApplication
{
    Task<Response<IEnumerable<CategoryDto>>> GetAllAsync();
}