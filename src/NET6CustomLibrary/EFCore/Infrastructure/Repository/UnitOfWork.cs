namespace NET6CustomLibrary.EFCore.Infrastructure.Repository;

public class UnitOfWork<TEntity, TKey> : IUnitOfWork<TEntity, TKey> where TEntity : class, IEntity<TKey>, new()
{
    public DbContext DbContext { get; }
    public IDatabaseRepository<TEntity, TKey> ReadOnly { get; }
    public ICommandRepository<TEntity, TKey> Command { get; }

    public UnitOfWork(DbContext dbContext, IDatabaseRepository<TEntity, TKey> databaseRepository, ICommandRepository<TEntity, TKey> commandRepository)
    {
        DbContext = dbContext;

        ReadOnly = databaseRepository;
        Command = commandRepository;
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