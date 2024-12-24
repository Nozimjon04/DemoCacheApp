using CacheDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace CacheDemo.Data;

public class Repository : IRepository
{
    protected readonly AppDbContext _dbContext;
    protected readonly DbSet<User> _dbSet;

    public Repository(AppDbContext dbContext)
    {
        this._dbContext = dbContext;
        this._dbSet = _dbContext.Set<User>();
    }


    public async Task<User> InsertAsync(User entity)
    {
        var entry = await this._dbSet.AddAsync(entity);

        await _dbContext.SaveChangesAsync();

        return entry.Entity;
    }


    public async Task<bool> DeleteAsync(long id)
    {
        var entity = await this._dbSet.FirstOrDefaultAsync(e => e.Id == id);
        _dbSet.Remove(entity);

        return await _dbContext.SaveChangesAsync() > 0;
    }


    public IQueryable<User> SelectAll()
        => this._dbSet;



    public async Task<User> SelectByIdAsync(long id)
        => await this._dbSet.FirstOrDefaultAsync(e => e.Id == id);



    public async Task<User> UpdateAsync(User entity)
    {
        var entry = this._dbContext.Update(entity);
        await this._dbContext.SaveChangesAsync();

        return entry.Entity;
    }
}
