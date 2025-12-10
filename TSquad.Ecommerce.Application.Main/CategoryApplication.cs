using System.Text;
using System.Text.Json;
using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using TSquad.Ecommerce.Application.DTO;
using TSquad.Ecommerce.Application.Interface;
using TSquad.Ecommerce.CrossCutting.Common;
using TSquad.Ecommerce.Domain.Interface;

namespace TSquad.Ecommerce.Application.Main;

public class CategoryApplication : ICategoryApplication
{
    private readonly ICategoryDomain _categoryDomain;
    private readonly IMapper _mapper;
    private readonly IDistributedCache _distributedCache;

    public CategoryApplication(ICategoryDomain categoryDomain,  IMapper mapper,  IDistributedCache distributedCache)
    {
        _categoryDomain = categoryDomain;
        _mapper = mapper;
        _distributedCache = distributedCache;
    }


    public async Task<Response<IEnumerable<CategoryDto>>> GetAllAsync()
    {
        var response = new Response<IEnumerable<CategoryDto>>();
        const string cacheKey = "categories";
        try
        {
            var redisCategories = await _distributedCache.GetStringAsync(cacheKey);
            if (redisCategories != null)
            {
                response.Data = JsonSerializer.Deserialize<IEnumerable<CategoryDto>>(redisCategories);
            }
            else
            {
                var categories = await _categoryDomain.GetAllAsync();
                response.Data = _mapper.Map<IEnumerable<CategoryDto>>(categories);
                var serializedCategories = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(categories));
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddHours(8))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(60));
                
                await _distributedCache.SetAsync(cacheKey, serializedCategories, options);
            }
            
            
            if (response.Data is not null)
            {
                response.IsSuccess = true;
                response.Message = "Consulta exitosa";
            }
        }
        catch (Exception e)
        {
            response.Message = e.Message;
        }
        return response;
    }
}