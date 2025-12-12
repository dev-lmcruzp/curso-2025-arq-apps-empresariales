using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SendGrid.Extensions.DependencyInjection;
using TSquad.Ecommerce.Application.Interface.Infrastructure;
using TSquad.Ecommerce.Infrastructure.EventBus;
using TSquad.Ecommerce.Infrastructure.EventBus.Options;
using TSquad.Ecommerce.Infrastructure.Sendmail;
using TSquad.Ecommerce.Infrastructure.Sendmail.Options;

namespace TSquad.Ecommerce.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureOptions<RabbitMqOptionsSetup>();
        services.AddScoped<IEventBus, EventBusRabbitMq>();
        services.AddMassTransit(opts =>
        {
            opts.UsingRabbitMq((context, cfg) =>
            {
                var rabbitMqOptions = services.BuildServiceProvider()
                    .GetRequiredService<IOptions<RabbitMqOptions>>()
                    .Value;

                cfg.Host(rabbitMqOptions.HostName, rabbitMqOptions.VirtualHost,
                    h =>
                    {
                        h.Username(rabbitMqOptions.UserName);
                        h.Password(rabbitMqOptions.Password);
                    });
                
                cfg.ConfigureEndpoints(context);
            });
        });

        services.AddScoped<ISendmail, SendgridMail>();
        services.ConfigureOptions<SendgridOptions>();
        var sendgridOptions = services.BuildServiceProvider()
            .GetRequiredService<IOptions<SendgridOptions>>()
            .Value;

        services.AddSendGrid(opts => { opts.ApiKey = sendgridOptions.ApiKey; });
        
        return services;
    }
}