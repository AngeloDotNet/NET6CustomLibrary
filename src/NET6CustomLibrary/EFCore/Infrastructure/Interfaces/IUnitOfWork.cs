namespace NET6CustomLibrary.EFCore.Infrastructure.Interfaces;

public interface IUnitOfWork<TEntity, TKey> : IDisposable where TEntity : class, IEntity<TKey>, new()
{
    IDatabaseRepository<TEntity, TKey> ReadOnly { get; }
    ICommandRepository<TEntity, TKey> Command { get; }
}