using TSquad.Ecommerce.Application.DTO.Enums;

namespace TSquad.Ecommerce.Application.DTO;

public sealed record DiscountDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Percent { get; set; }
    public DiscountStatus Status { get; set; }
}