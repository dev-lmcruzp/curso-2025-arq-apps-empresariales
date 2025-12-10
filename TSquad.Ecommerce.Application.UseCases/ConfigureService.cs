using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TSquad.Ecommerce.Application.Interface.UseCases;
using TSquad.Ecommerce.Application.UseCases.Categories;
using TSquad.Ecommerce.Application.UseCases.Customers;
using TSquad.Ecommerce.Application.UseCases.Users;
using TSquad.Ecommerce.CrossCutting.Common;

namespace TSquad.Ecommerce.Application.UseCases;

public static class ConfigureService
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICustomerApplication, CustomerApplication>();
        services.AddScoped<IAuthApplication, AuthApplication>();
        services.AddScoped<ICategoryApplication, CategoryApplication>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        return services;
    }
}