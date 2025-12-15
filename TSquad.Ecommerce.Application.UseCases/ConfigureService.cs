using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TSquad.Ecommerce.Application.Interface.UseCases;
using TSquad.Ecommerce.Application.UseCases.Categories;
using TSquad.Ecommerce.Application.UseCases.Customers;
using TSquad.Ecommerce.Application.UseCases.Discounts;
using TSquad.Ecommerce.Application.UseCases.Users;
using TSquad.Ecommerce.Application.Validator;
using TSquad.Ecommerce.CrossCutting.Common;

namespace TSquad.Ecommerce.Application.UseCases;

public static class ConfigureService
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        
        services.AddScoped<ICustomerApplication, CustomerApplication>();
        services.AddScoped<IAuthApplication, AuthApplication>();
        services.AddScoped<ICategoryApplication, CategoryApplication>();
        services.AddScoped<IDiscountApplication, DiscountApplication>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        AddValidator(services);
        return services;
    }
    
    private static void AddValidator(IServiceCollection services)
    {
        services.AddTransient<SignInValidator>();
        services.AddTransient<DiscountDtoValidator>();
    }
}