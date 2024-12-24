using CacheDemo.Entities;

namespace CacheDemo.Service;

public interface IUserService
{
    public Task<User> CreateAsync(User user);
}
