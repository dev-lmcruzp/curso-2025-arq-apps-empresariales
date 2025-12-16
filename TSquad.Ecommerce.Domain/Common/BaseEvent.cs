namespace TSquad.Ecommerce.Domain.Common;

public abstract class BaseEvent
{
    public Guid MessageId { get; set; } = Guid.NewGuid();
    public DateTime PublishTime { get; set; } = DateTime.UtcNow;
}