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

    //public async Task CreateTransactionAsync(TEntity entity)
    //{
    //    using var transaction = await DbContext.Database.BeginTransactionAsync();

    //    try
    //    {
    //        DbContext.Set<TEntity>().Add(entity);
    //        await DbContext.SaveChangesAsync();
    //        DbContext.Entry(entity).State = EntityState.Detached;

    //        await transaction.CommitAsync();
    //    }
    //    catch (Exception)
    //    {
    //        await transaction.RollbackAsync();
    //        throw;
    //    }
    //}

    //public async Task UpdateTransactionAsync(TEntity entity)
    //{
    //    using var transaction = await DbContext.Database.BeginTransactionAsync();

    //    try
    //    {
    //        DbContext.Set<TEntity>().Update(entity);
    //        await DbContext.SaveChangesAsync();
    //        DbContext.Entry(entity).State = EntityState.Detached;

    //        transaction.Commit();
    //    }
    //    catch (Exception)
    //    {
    //        await transaction.RollbackAsync();
    //        throw;
    //    }
    //}

    //public async Task DeleteTransactionAsync(TEntity entity)
    //{
    //    using var transaction = await DbContext.Database.BeginTransactionAsync();

    //    try
    //    {
    //        DbContext.Set<TEntity>().Remove(entity);
    //        await DbContext.SaveChangesAsync();

    //        transaction.Commit();
    //    }
    //    catch (Exception)
    //    {
    //        await transaction.RollbackAsync();
    //        throw;
    //    }
    //}
}