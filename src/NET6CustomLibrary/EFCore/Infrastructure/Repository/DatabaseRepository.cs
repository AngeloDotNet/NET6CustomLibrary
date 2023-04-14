namespace NET6CustomLibrary.EFCore.Infrastructure.Repository;

public class DatabaseRepository<TEntity, TKey> : Database<TEntity, TKey>, IDatabaseRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>, new()
{
    public DatabaseRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public async Task<int> GetCountAsync()
    {
        return await DbContext.Set<TEntity>()
            .CountAsync();
    }

    public async Task<List<TEntity>> GetOrderByIdAscendingAsync()
    {
        return await DbContext.Set<TEntity>()
            .OrderBy(x => x.Id)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<TEntity>> GetOrderByIdDescendingAsync()
    {
        return await DbContext.Set<TEntity>()
            .OrderByDescending(x => x.Id)
            .AsNoTracking()
            .ToListAsync();
    }
}