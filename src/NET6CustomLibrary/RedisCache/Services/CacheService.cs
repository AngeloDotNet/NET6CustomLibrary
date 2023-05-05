namespace NET6CustomLibrary.RedisCache.Services;

public class CacheService : ICacheService
{
    private readonly IDistributedCache cache;
    private readonly IOptionsMonitor<RedisOptions> redisOptionsMonitor;

    public CacheService(IDistributedCache cache, IOptionsMonitor<RedisOptions> redisOptionsMonitor)
    {
        this.cache = cache;
        this.redisOptionsMonitor = redisOptionsMonitor;
    }

    /// <summary>
    /// Get the values from the cache
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns>Return the values from the cache</returns>
    public T GetCache<T>(string key)
    {
        var value = cache.GetString(key);

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
    /// <returns>Return the values after set the values in the cache</returns>
    public T SetCache<T>(string key, T value)
    {
        var options = redisOptionsMonitor.CurrentValue;
        var redisOptions = new DistributedCacheEntryOptions
        {
            //Set the time the cache will expire from the time of entry (representing now)
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(options.AbsoluteExpireTime),

            //Time until the cache entry is valid, before which if a hit occurs the time must be extended further.
            SlidingExpiration = TimeSpan.FromMinutes(options.SlidingExpireTime)
        };

        cache.SetString(key, JsonConvert.SerializeObject(value), redisOptions);

        return value;
    }
}