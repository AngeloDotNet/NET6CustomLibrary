namespace NET6CustomLibrary.EFCore.Core;

public class Database<TEntity, TKey> : IDatabase<TEntity, TKey> where TEntity : class, IEntity<TKey>, new()
{
    public DbContext DbContext { get; }

    public Database(DbContext dbContext)
    {
        DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await DbContext.Set<TEntity>()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(TKey id)
    {
        var entity = await DbContext.Set<TEntity>()
            .FindAsync(id);

        if (entity == null)
        {
            return null;
        }

        DbContext.Entry(entity).State = EntityState.Detached;

        return entity;
    }
}