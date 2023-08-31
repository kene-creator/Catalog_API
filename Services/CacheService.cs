
using System.Text.Json;
using StackExchange.Redis;

namespace Catalog_API.Services;

public class CacheService : ICacheService
{
    private IDatabase _cacheDb;

    public CacheService()
    {
        var redis = ConnectionMultiplexer.Connect("localhost:6379");
    }
    public T GetData<T>(string key)
    {
        var value = _cacheDb.StringGet(key);
        return !string.IsNullOrEmpty(value) ? JsonSerializer.Deserialize<T>(value) : default;
    }

    public bool SetData<T>(string key, T value, DateTimeOffset ttl)
    {
        var expirationTime = ttl.DateTime.Subtract(DateTime.Now);
        var isSet = _cacheDb.StringSet(key, JsonSerializer.Serialize(value), expirationTime);
        return isSet;
    }
    
    public object RemoveData(string key)
    {
        var _exist = _cacheDb.KeyExists(key);
        return _exist ? _cacheDb.KeyDelete(key) : (object)false;
    }

}