namespace NET6CustomLibrary.EFCoreTransaction.Infrastructure;

public class TUnitOfWork<TEntity, TKey> : ITUnitOfWork<TEntity, TKey> where TEntity : class, ITEntity<TKey>, new()
{
    public DbContext DbContext { get; }
    public IRepository<TEntity, TKey> Repository { get; }

    public TUnitOfWork(DbContext dbContext, IRepository<TEntity, TKey> repository)
    {
        DbContext = dbContext;
        Repository = repository;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            DbContext.Dispose();
        }
    }
}