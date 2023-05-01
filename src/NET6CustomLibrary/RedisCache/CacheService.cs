namespace NET6CustomLibrary.RedisCache;

public class CacheService : ICacheService
{
    private readonly IDistributedCache _cache;

    public CacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    /// <summary>
    /// Get the values from the cache
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    public T Get<T>(string key)
    {
        var value = _cache.GetString(key);

        if (value != null)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        return default;
    }

    /// <summary>
    /// Set the values in the cache
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="absoluteExpireTime"></param>
    /// <param name="slidingExpireTime"></param>
    /// <returns></returns>
    public T Set<T>(string key, T value, TimeSpan? absoluteExpireTime = null, TimeSpan? slidingExpireTime = null)
    {
        var options = new DistributedCacheEntryOptions
        {
            // Imposta l'ora in cui la cache scadrà a partire dall'ora di inserimento (che rappresenta l'adesso)
            AbsoluteExpirationRelativeToNow = absoluteExpireTime,

            // Tempo fino al quale la voce della cache è valida, prima del quale se si verifica un hit il tempo deve essere esteso ulteriormente.
            SlidingExpiration = slidingExpireTime
        };

        _cache.SetString(key, JsonConvert.SerializeObject(value), options);

        return value;
    }
}