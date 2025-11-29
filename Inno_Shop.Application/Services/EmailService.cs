using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using MimeKit;

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
        var message = new MailMessage();
        message.From = new MailAddress(_configuration["Email:SenderEmail"], _configuration["Email:SenderName"]);
        message.To.Add(email);
        message.Subject = subject;
        message.Body = body;
        message.IsBodyHtml = true;

        using var smtp = new SmtpClient(_configuration["Email:Host"], int.Parse(_configuration["Email:Port"]))
        {
            Credentials = new NetworkCredential(_configuration["Email:SenderEmail"], _configuration["Email:Password"]),
            EnableSsl = bool.Parse(_configuration["Email:EnableSsl"])
        };

        await smtp.SendMailAsync(message);
    }
}