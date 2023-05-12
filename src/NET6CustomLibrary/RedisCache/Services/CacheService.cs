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

    public async Task<T> GetCacheAsync<T>(string key)
    {
        var jsonData = await cache.GetStringAsync(key);

        if (jsonData is null)
        {
            return default;
        }

        return System.Text.Json.JsonSerializer.Deserialize<T>(jsonData);
    }

    public async Task<T> SetCacheAsync<T>(string key, T value)
    {
        var optionsCache = redisOptionsMonitor.CurrentValue;
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = optionsCache.AbsoluteExpireTime,
            SlidingExpiration = optionsCache.SlidingExpireTime
        };

        var jsonData = System.Text.Json.JsonSerializer.Serialize(value);

        await cache.SetStringAsync(key, jsonData, options);

        return value;
    }
}