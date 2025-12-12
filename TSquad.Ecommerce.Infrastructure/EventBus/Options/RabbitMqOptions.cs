namespace TSquad.Ecommerce.Infrastructure.EventBus.Options;

public class RabbitMqOptions
{
    public string HostName { get; init; } = null!;
    public string VirtualHost { get; init; } = null!;
    public string UserName { get; init; } = null!;
    public string Password { get; init; } = null!;
}