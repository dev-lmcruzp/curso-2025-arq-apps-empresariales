using TSquad.Ecommerce.Application.Validator;

namespace TSquad.Ecommerce.Service.WebApi.Modules.Validator;

/// <summary>
/// 
/// </summary>
public static class ValidatorExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddValidator(this IServiceCollection services)
    {
        services.AddTransient<SignInValidator>();
        return services;
    }
}