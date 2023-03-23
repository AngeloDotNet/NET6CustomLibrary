namespace NET6CustomLibrary.EFCore.Infrastructure.Repository;

public class CommandRepository<TEntity, TKey> : Command<TEntity, TKey>, ICommandRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>, new()
{
    public CommandRepository(DbContext dbContext) : base(dbContext)
    {
    }
}