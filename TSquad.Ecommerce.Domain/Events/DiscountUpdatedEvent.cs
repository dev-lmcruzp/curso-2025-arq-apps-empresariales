using TSquad.Ecommerce.Domain.Common;
using TSquad.Ecommerce.Domain.Enums;

namespace TSquad.Ecommerce.Domain.Events;

public class DiscountUpdatedEvent : BaseEvent
{
    public int Id { get; set; }
    public decimal Percent { get; set; }
    public DiscountStatus Status { get; set; }
}