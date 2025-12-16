using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TSquad.Ecommerce.Application.Interface.UseCases;
using TSquad.Ecommerce.Application.UseCases.Categories;
using TSquad.Ecommerce.Application.UseCases.Commons.Behaviors;
using TSquad.Ecommerce.Application.UseCases.Customers;
using TSquad.Ecommerce.Application.UseCases.Discounts;
using TSquad.Ecommerce.Application.UseCases.Users;
using TSquad.Ecommerce.CrossCutting.Common;

namespace TSquad.Ecommerce.Application.UseCases;

public static class ConfigureService
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviors<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviors<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviors<,>));
        });
        
        services.AddScoped<ICustomerApplication, CustomerApplication>();
        services.AddScoped<IAuthApplication, AuthApplication>();
        services.AddScoped<ICategoryApplication, CategoryApplication>();
        services.AddScoped<IDiscountApplication, DiscountApplication>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        return services;
    }
}