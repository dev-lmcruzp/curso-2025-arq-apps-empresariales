using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace TSquad.Ecommerce.Infrastructure.Sendmail.Options;

public class SendgridOptionsSetup : IConfigureOptions<SendgridOptions>
{
    private const string ConfigurationSectionName = "SendGrid";
    private readonly IConfiguration _configuration;
    public SendgridOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void Configure(SendgridOptions options)
    {
        _configuration.GetSection(ConfigurationSectionName).Bind(options);
    }
}