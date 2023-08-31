namespace Catalog_API.Services;

public interface ICacheService
{
    T GetData<T>(string key);
    bool SetData<T>(string key, T value, DateTimeOffset ttl);
    object RemoveData(string key);
}