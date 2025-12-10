namespace TSquad.Ecommerce.Application.DTO;

public sealed record SignInDto
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}