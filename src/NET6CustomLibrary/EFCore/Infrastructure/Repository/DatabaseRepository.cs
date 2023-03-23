namespace NET6CustomLibrary.EFCore.Infrastructure.Repository;

public class DatabaseRepository<TEntity, TKey> : Database<TEntity, TKey>, IDatabaseRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>, new()
{
    public DatabaseRepository(DbContext dbContext) : base(dbContext)
    {
    }
}