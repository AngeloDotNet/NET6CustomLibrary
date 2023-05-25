namespace NET6CustomLibrary.EFCoreTransaction.Core;

public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, ITEntity<TKey>, new()
{
    public DbContext DbContext { get; }

    public Repository(DbContext dbContext) => DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public async Task CreateTAsync(TEntity entity)
    {
        using var transaction = await DbContext.Database.BeginTransactionAsync();

        try
        {
            DbContext.Set<TEntity>().Add(entity);
            await DbContext.SaveChangesAsync();
            DbContext.Entry(entity).State = EntityState.Detached;

            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task UpdateTAsync(TEntity entity)
    {
        using var transaction = await DbContext.Database.BeginTransactionAsync();

        try
        {
            DbContext.Set<TEntity>().Update(entity);
            await DbContext.SaveChangesAsync();
            DbContext.Entry(entity).State = EntityState.Detached;

            transaction.Commit();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task DeleteTAsync(TEntity entity)
    {
        using var transaction = await DbContext.Database.BeginTransactionAsync();

        try
        {
            DbContext.Set<TEntity>().Remove(entity);
            await DbContext.SaveChangesAsync();

            transaction.Commit();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<List<TEntity>> GetAllTAsync() => await DbContext.Set<TEntity>()
        .AsNoTracking().ToListAsync();

    public async Task<TEntity> GetByIdTAsync(TKey id)
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

    public async Task<List<TEntity>> GetOrderByIdAscendingTAsync() => await DbContext.Set<TEntity>()
        .OrderBy(x => x.Id).AsNoTracking().ToListAsync();

    public async Task<List<TEntity>> GetOrderByIdDescendingTAsync() => await DbContext.Set<TEntity>()
        .OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();

    public async Task<int> GetCountTAsync() => await DbContext.Set<TEntity>().CountAsync();
}