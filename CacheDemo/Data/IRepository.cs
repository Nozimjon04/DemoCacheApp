using CacheDemo.Entities;

namespace CacheDemo.Data;

public interface IRepository
{
    Task<bool> DeleteAsync(long id);
    IQueryable<User> SelectAll();
    Task<User> SelectByIdAsync(long id);
    Task<User> InsertAsync(User user);
    Task<User> UpdateAsync(User entity);
}
