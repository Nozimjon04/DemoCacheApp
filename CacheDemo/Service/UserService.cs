using CacheDemo.Data;
using CacheDemo.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CacheDemo.Service;

public class UserService : IUserService
{
    private readonly IEmailService emailService;
    private readonly IRepository repository;
    private readonly IMemoryCache memoryCache;


    public UserService(IRepository repository, IEmailService emailService, IMemoryCache memoryCache)
    {

        this.repository = repository;
        this.emailService = emailService;
        this.memoryCache = memoryCache;

    }
    public async Task<User> CreateAsync(User user)
    {
        var person = await this.repository.SelectAll().
            Where(u => u.Email == user.Email).
            FirstOrDefaultAsync();
        if (person is not null)
        {
            throw new  Exception("Person is already exist");
        }

        await this.repository.InsertAsync(user);

        return user;

       
    }
}
