using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TSquad.Ecommerce.Domain.Entity;
using TSquad.Ecommerce.Infrastructure.Data;
using TSquad.Ecommerce.Infrastructure.Interface;

namespace TSquad.Ecommerce.Infrastructure.Repository;

public static class ConfigureService
{
    public static IServiceCollection AddRepository(this IServiceCollection services)
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