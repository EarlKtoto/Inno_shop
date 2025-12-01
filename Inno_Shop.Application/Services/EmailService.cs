using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace Application.Services;

public class EmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string email, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_configuration["EmailSettings:MailName"], _configuration["EmailSettings:MailAddress"]));
        message.To.Add(MailboxAddress.Parse(email));
        message.Subject = subject;
        message.Body = new TextPart(TextFormat.Html) { Text = body };
        
        using var client = new SmtpClient();
            await client.ConnectAsync(_configuration["EmailSettings:Host"], int.Parse(_configuration["EmailSettings:Port"]), SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_configuration["EmailSettings:MailAddress"], _configuration["EmailSettings:Password"]);
            await client.SendAsync(message);

        await client.DisconnectAsync(true);
    }
}