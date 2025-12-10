namespace TSquad.Ecommerce.Application.DTO;

public sealed record TokenDto
{
    public string AccessToken { get; set; } = null!;
    public string TokenType { get; set; } = null!;
    public int ExpiresIn { get; set; }
}