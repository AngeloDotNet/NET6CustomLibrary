namespace NET6CustomLibrary.EFCore.Core.Interfaces;

public interface ICommand<TEntity, TKey> where TEntity : class, IEntity<TKey>, new()
{
    /// <summary>
    /// Create an item.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task CreateAsync(TEntity entity);

    /// <summary>
    /// Update an item.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task UpdateAsync(TEntity entity);

    /// <summary>
    /// Delete an item.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task DeleteAsync(TEntity entity);
}