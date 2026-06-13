using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using Trainova.Application.Common.Interfaces.Services;

namespace Trainova.Infrastructure.Cacheing.strategies
{
    public class RedisCacheStrategy<TValue> : ICacheService<TValue>
    {
        private readonly IDistributedCache _distributedCache;

        public RedisCacheStrategy(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<TValue?> GetAsync(string cacheKey)
        {
            var cachedData = await _distributedCache.GetStringAsync(cacheKey);

            if (string.IsNullOrEmpty(cachedData))
            {
                return default;
            }

            return JsonSerializer.Deserialize<TValue>(cachedData);
        }

        public async Task SetAsync<TValue>(string cacheKey, TValue value, TimeSpan? expiration = null)
        {
            var jsonData = JsonSerializer.Serialize(value);

            var options = new DistributedCacheEntryOptions();

            if (expiration.HasValue)
            {
                options.SetAbsoluteExpiration(expiration.Value);
            }
            else
            {
                options.SetAbsoluteExpiration(TimeSpan.FromHours(3));
            }

            await _distributedCache.SetStringAsync(cacheKey, jsonData, options);
        }

    }
}
