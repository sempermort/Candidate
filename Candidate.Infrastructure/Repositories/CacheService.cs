using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace Candidate.Application.Services
{
    public class CacheService : ICacheService
        {
            private readonly IMemoryCache _memoryCache;
            private readonly IDistributedCache _distributedCache;

            public CacheService(IMemoryCache memoryCache, IDistributedCache distributedCache)
            {
                _memoryCache = memoryCache;
                _distributedCache = distributedCache;
            }

            public T Get<T>(string key)
            {
                return _memoryCache.TryGetValue(key, out T value) ? value : default;
            }

            public async Task<T> GetAsync<T>(string key)
            {
                var value = await _distributedCache.GetStringAsync(key);
                return value != null ? JsonSerializer.Deserialize<T>(value) : default;
            }

            public void Set<T>(string key, T value, TimeSpan expiration)
            {
                _memoryCache.Set(key, value, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = expiration
                });
            }

            public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
            {
                var serializedValue = JsonSerializer.Serialize(value);
                await _distributedCache.SetStringAsync(key, serializedValue, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = expiration
                });
            }

            public void Remove(string key)
            {
                _memoryCache.Remove(key);
            }

            public async Task RemoveAsync(string key)
            {
                await _distributedCache.RemoveAsync(key);
            }
        }
    }


