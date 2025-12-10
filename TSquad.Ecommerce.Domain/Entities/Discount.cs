using TSquad.Ecommerce.Domain.Common;
using TSquad.Ecommerce.Domain.Enums;

namespace TSquad.Ecommerce.Domain.Entities;

public class Discount : BaseAuditableEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Percent { get; set; }
    public DiscountStatus Status { get; set; }
}