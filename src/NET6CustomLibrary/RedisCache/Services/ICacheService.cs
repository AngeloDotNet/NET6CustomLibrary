namespace NET6CustomLibrary.RedisCache.Services;

public interface ICacheService
{
    Task<T> GetCacheAsync<T>(string key);
    Task<T> SetCacheAsync<T>(string key, T value);
}