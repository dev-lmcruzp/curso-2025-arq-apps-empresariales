using TSquad.Ecommerce.Domain.Common;

namespace TSquad.Ecommerce.Domain.Events;

public class DiscountDeletedEvent : BaseEvent
{
    public int Id { get; set; }
}