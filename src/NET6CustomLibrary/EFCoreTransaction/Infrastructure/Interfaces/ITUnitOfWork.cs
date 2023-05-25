namespace NET6CustomLibrary.EFCoreTransaction.Infrastructure.Interfaces;

public interface ITUnitOfWork<TEntity, TKey> : IDisposable where TEntity : class, ITEntity<TKey>, new()
{
    IRepository<TEntity, TKey> Repository { get; }
}