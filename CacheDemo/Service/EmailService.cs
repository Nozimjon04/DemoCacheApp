using CacheDemo.Entities;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Caching.Memory;

namespace CacheDemo.Service;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly IMemoryCache memoryCache;

    public EmailService(IConfiguration configuration, IMemoryCache memoryCache)
    {
        _configuration = configuration.GetSection("Email");
        this.memoryCache = memoryCache;
    }

    public async Task<bool> GetCodeAsync(string email, string code)
    {
        var cashedValue = memoryCache.Get<string>(email);

        if (cashedValue?.ToString() == code)
        {
            return true;
        }

        return false;
    }

    public async Task SendMessage(Message message)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_configuration["EmailAddress"]));
        email.To.Add(MailboxAddress.Parse(message.To));

        email.Subject = message.Subject;
        email.Body = new TextPart("html")
        {
            Text = message.Body
        };

        var smtp = new SmtpClient();

        await smtp.ConnectAsync(_configuration["Host"], 587, MailKit.Security.SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_configuration["EmailAddress"], _configuration["Password"]);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }

    public async Task<bool> VerifyEmailAsync(string email)
    {
        var randomNumber = new Random().Next(100000, 999999);

        var message = new Message()
        {
            Subject = "Do not give this code to Others",
            To = email,
            Body = $"{randomNumber}"
        };

        memoryCache.Set(email, randomNumber.ToString(), TimeSpan.FromSeconds(10));
        await this.SendMessage(message);

        return true;
    }
}
