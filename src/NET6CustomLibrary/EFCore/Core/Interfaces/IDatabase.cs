namespace NET6CustomLibrary.EFCore.Core.Interfaces;

public interface IDatabase<TEntity, TKey> where TEntity : class, IEntity<TKey>, new()
{
    /// <summary>
    /// Get the list of items.
    /// </summary>
    /// <returns></returns>
    Task<List<TEntity>> GetAllAsync();

    /// <summary>
    /// Gets the item with the specified identifier.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<TEntity> GetByIdAsync(TKey id);
}