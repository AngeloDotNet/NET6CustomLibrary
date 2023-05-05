namespace NET6CustomLibrary.RedisCache.Services;

public interface ICacheService
{
    T GetCache<T>(string key);
    T SetCache<T>(string key, T value);
}