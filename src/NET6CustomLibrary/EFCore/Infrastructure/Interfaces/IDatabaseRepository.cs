namespace NET6CustomLibrary.EFCore.Infrastructure.Interfaces;

public interface IDatabaseRepository<TEntity, TKey> : IDatabase<TEntity, TKey> where TEntity : class, IEntity<TKey>, new()
{
    Task<List<TEntity>> GetOrderByIdAscendingAsync();
    Task<List<TEntity>> GetOrderByIdDescendingAsync();
    Task<int> GetCountAsync();
}