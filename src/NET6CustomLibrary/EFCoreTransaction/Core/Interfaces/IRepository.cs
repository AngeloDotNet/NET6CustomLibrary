namespace NET6CustomLibrary.EFCoreTransaction.Core.Interfaces;

public interface IRepository<TEntity, TKey> where TEntity : class, ITEntity<TKey>, new()
{
    //GET
    Task<List<TEntity>> GetAllTAsync();
    Task<TEntity> GetByIdTAsync(TKey id);
    Task<List<TEntity>> GetOrderByIdAscendingTAsync();
    Task<List<TEntity>> GetOrderByIdDescendingTAsync();
    Task<int> GetCountTAsync();

    // SET
    Task CreateTAsync(TEntity entity);
    Task UpdateTAsync(TEntity entity);
    Task DeleteTAsync(TEntity entity);
}