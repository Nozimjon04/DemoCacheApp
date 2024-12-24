using CacheDemo.Entities;

namespace CacheDemo.Service;

public interface IEmailService
{
    public Task SendMessage(Message message);

    public Task<bool> VerifyEmailAsync(string email);

    public Task<bool> GetCodeAsync(string email, string code);
}