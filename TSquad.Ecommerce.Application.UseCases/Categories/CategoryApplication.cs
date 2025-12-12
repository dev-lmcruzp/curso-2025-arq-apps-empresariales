using System.Text;
using System.Text.Json;
using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using TSquad.Ecommerce.Application.DTO;
using TSquad.Ecommerce.Application.Interface.Persistence;
using TSquad.Ecommerce.Application.Interface.UseCases;
using TSquad.Ecommerce.CrossCutting.Common;

namespace TSquad.Ecommerce.Application.UseCases.Categories;

public class CategoryApplication : ICategoryApplication
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IDistributedCache _distributedCache;

    public CategoryApplication(IUnitOfWork unitOfWork,  IMapper mapper,  IDistributedCache distributedCache)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _distributedCache = distributedCache;
    }


    public async Task<Response<IEnumerable<CategoryDto>>> GetAllAsync()
    {
        var response = new Response<IEnumerable<CategoryDto>>();
        const string cacheKey = "categories";
        var redisCategories = await _distributedCache.GetStringAsync(cacheKey);
        if (redisCategories != null)
        {
            response.Data = JsonSerializer.Deserialize<IEnumerable<CategoryDto>>(redisCategories);
        }
        else
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();
            response.Data = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            var serializedCategories = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(categories));
            var options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(DateTime.Now.AddHours(8))
                .SetSlidingExpiration(TimeSpan.FromMinutes(60));
                
            await _distributedCache.SetAsync(cacheKey, serializedCategories, options);
        }


        if (response.Data is null)
        {
            response.Message = "Sin registros";
            return response;
        }
        
        
        response.IsSuccess = true;
        response.Message = "Consulta exitosa";
        
        return response;
    }
}