using Microsoft.EntityFrameworkCore;

namespace Loclep.Generic.EFCore;

public class BaseRepository<TDbContext, T, TId> : IBaseRepository<T, TId>
  where TDbContext : DbContext
  where T : class
  where TId : struct
{
  protected readonly TDbContext _dbContext;

  public BaseRepository(TDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<T> Create(T entity)
  {
    await _dbContext.Set<T>().AddAsync(entity);
    await _dbContext.SaveChangesAsync();
    return entity;
  }

  public async Task Delete(TId id)
  {
    T entity = await GetById(id);
    _dbContext.Set<T>().Remove(entity);
    await _dbContext.SaveChangesAsync();
  }

  public async Task<IEnumerable<T>> GetAll()
  {
    IEnumerable<T> entities = await _dbContext.Set<T>().ToListAsync();
    return entities;
  }

  public async Task<T> GetById(TId id)
  {
    T entity = await _dbContext.Set<T>().FindAsync(id);
    return entity;
  }

  public async Task<T> Update(T entity)
  {
    _dbContext.Set<T>().Update(entity);
    await _dbContext.SaveChangesAsync();
    return entity;
  }
}
