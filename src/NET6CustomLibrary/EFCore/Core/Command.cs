namespace NET6CustomLibrary.EFCore.Core;

public class Command<TEntity, TKey> : ICommand<TEntity, TKey> where TEntity : class, IEntity<TKey>, new()
{
    public DbContext DbContext { get; }

    public Command(DbContext dbContext)
    {
        DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task CreateAsync(TEntity entity)
    {
        DbContext.Set<TEntity>().Add(entity);

        await DbContext.SaveChangesAsync();

        DbContext.Entry(entity).State = EntityState.Detached;
    }

    public async Task UpdateAsync(TEntity entity)
    {
        DbContext.Set<TEntity>().Update(entity);

        await DbContext.SaveChangesAsync();

        DbContext.Entry(entity).State = EntityState.Detached;
    }

    public async Task DeleteAsync(TEntity entity)
    {
        DbContext.Set<TEntity>().Remove(entity);

        await DbContext.SaveChangesAsync();
    }
}