using TSquad.Ecommerce.Domain.Entity;
using TSquad.Ecommerce.Domain.Interface;
using TSquad.Ecommerce.Infrastructure.Interface;

namespace TSquad.Ecommerce.Domain.Core;

public class CategoryDomain : ICategoryDomain
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryDomain(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _unitOfWork.Categories.GetAllAsync();
    }
}