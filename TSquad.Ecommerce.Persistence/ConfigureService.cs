using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TSquad.Ecommerce.Domain;
using TSquad.Ecommerce.Application.Interface.Persistence;
using TSquad.Ecommerce.Domain.Entities;
using TSquad.Ecommerce.Persistence.Contexts;
using TSquad.Ecommerce.Persistence.Repositories;

namespace TSquad.Ecommerce.Persistence;

public static class ConfigureService
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddSingleton<DapperContext>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        return services;
    }
}