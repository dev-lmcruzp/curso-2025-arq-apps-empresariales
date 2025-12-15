using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using TSquad.Ecommerce.Application.Interface.Infrastructure;
using TSquad.Ecommerce.Infrastructure.Sendmail.Options;
using SendGrid.Helpers.Mail;

namespace TSquad.Ecommerce.Infrastructure.Sendmail;

public class SendgridMail : ISendmail
{
    private readonly ILogger<SendgridMail> _logger;
    private readonly SendgridOptions _sendgridOptions;
    private readonly ISendGridClient _sendGridClient;

    public SendgridMail(ILogger<SendgridMail> logger, IOptions<SendgridOptions> sendgridOptions, ISendGridClient sendGridClient)
    {
        _logger = logger;
        _sendgridOptions = sendgridOptions.Value;
        _sendGridClient = sendGridClient;
    }
    
    public async Task<bool> SendEmailAsync(string subject, string body, CancellationToken cancellationToken = default)
    {
        return true;
        var message = BuildMessage(subject, body);
        var response = await _sendGridClient.SendEmailAsync(message, cancellationToken).ConfigureAwait(false);

        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation($"Email sent to {_sendgridOptions.ToAddress} at {response.Headers.Date}");
            return true;
        }

        _logger.LogError($"Email failed to {_sendgridOptions.ToAddress} with error code {response.StatusCode}");
        return false;
    }

    private SendGridMessage BuildMessage(string subject, string body)
    {
        var message = new SendGridMessage
        {
            From = new EmailAddress(_sendgridOptions.FromEmail, _sendgridOptions.FromUser),
            Subject = subject
        };

        message.AddContent(MimeType.Text, body);
        message.AddTo(new EmailAddress(_sendgridOptions.ToAddress, _sendgridOptions.ToUser));

        if (_sendgridOptions.SandboxMode)
        {
            message.MailSettings = new MailSettings { SandboxMode = new SandboxMode { Enable = true } };
        }

        return message;
    }
}