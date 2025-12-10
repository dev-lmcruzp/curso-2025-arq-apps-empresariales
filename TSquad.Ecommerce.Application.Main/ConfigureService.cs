using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TSquad.Ecommerce.Application.Interface;
using TSquad.Ecommerce.CrossCutting.Common;

namespace TSquad.Ecommerce.Application.Main;

public static class ConfigureService
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        services.AddScoped<ICustomerApplication, CustomerApplication>();
        services.AddScoped<IAuthApplication, AuthApplication>();
        services.AddScoped<ICategoryApplication, CategoryApplication>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        return services;
    }
}