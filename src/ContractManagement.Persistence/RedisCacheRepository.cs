using ContractManagement.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace ContractManagement.Persistence
{
    public class RedisCacheRepository(IDistributedCache cache) : IRedisCacheRepository
    {
        private readonly IDistributedCache _cache = cache;

        public T? GetData<T>(string key)
        {
            var data = _cache?.GetString(key);

            if (data is null)
            {
                return default;
            }
            return JsonSerializer.Deserialize<T>(data)!;
        }

        public void SetData<T>(string key, T value)
        {
            var options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            };
            _cache.SetString(key, JsonSerializer.Serialize(value), options);
        }
    }
}
