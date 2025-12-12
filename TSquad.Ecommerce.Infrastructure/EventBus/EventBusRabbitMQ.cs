using MassTransit;
using TSquad.Ecommerce.Application.Interface.Infrastructure;

namespace TSquad.Ecommerce.Infrastructure.EventBus;

public class EventBusRabbitMq : IEventBus
{
    private readonly IPublishEndpoint _publishEndpoint;

    public EventBusRabbitMq(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async void Publish<T>(T @event)
    {
        await _publishEndpoint.Publish(@event!);
    }
}