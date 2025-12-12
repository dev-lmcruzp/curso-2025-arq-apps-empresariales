namespace TSquad.Ecommerce.Application.Interface.Infrastructure;

public interface ISendmail
{
    Task<bool>  SendEmailAsync(string subject, string body, CancellationToken cancellationToken = default);
}