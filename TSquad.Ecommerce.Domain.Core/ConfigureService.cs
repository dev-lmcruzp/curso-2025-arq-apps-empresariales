using Microsoft.Extensions.DependencyInjection;
using TSquad.Ecommerce.Domain.Interface;

namespace TSquad.Ecommerce.Domain.Core;

public static class ConfigureService
{
    // Microsoft.Extensions.DependencyInjection
    public static IServiceCollection AddDomainService(this IServiceCollection services)
    {
        services.AddScoped<ICustomerDomain, CustomerDomain>();
        services.AddScoped<IUserDomain, UserDomain>();
        services.AddScoped<ICategoryDomain, CategoryDomain>();
        return services;
    }
}