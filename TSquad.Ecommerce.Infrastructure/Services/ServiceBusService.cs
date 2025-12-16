using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TSquad.Ecommerce.Application.Interface.Infrastructure;

namespace TSquad.Ecommerce.Infrastructure.Services;

public class ServiceBusService : IServiceBusService
{
    private readonly ServiceBusClient _serviceBusClient;
    private readonly ILogger<IServiceBusService> _logger;

    public ServiceBusService(IConfiguration configuration, ILogger<IServiceBusService> logger)
    {
        var connectionString = configuration.GetConnectionString("ServiceBus") ?? configuration["ServiceBus"];
        _serviceBusClient = new ServiceBusClient(connectionString);
        _logger = logger;
    }

    public async Task SendMessageAsync<T>(string queueName, T message) where T : class
    {
        var sender = _serviceBusClient.CreateSender(queueName);
        var body = JsonSerializer.Serialize(message);

        var serviceBusMessage = new ServiceBusMessage(body)
        {
            ContentType = "application/json",
            Subject = typeof(T).Name,
            MessageId = Guid.NewGuid().ToString(),
        };

        await sender.SendMessageAsync(serviceBusMessage);
        _logger.LogInformation("Message sent to {QueueName}", queueName);
        await sender.DisposeAsync();
    }
}