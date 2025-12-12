namespace TSquad.Ecommerce.Infrastructure.Sendmail.Options;

public class SendgridOptions
{
    public string ApiKey { get; init; } = null!;
    public string FromEmail { get; init; } = null!;
    public string FromUser { get; init; } = null!;
    public bool SandboxMode { get; init; }
    public string ToAddress { get; init; } = null!;
    public string ToUser { get; init; } = null!;
}