using Trainova.Application.Common.Interfaces.Services;
using Trainova.Infrastructure.Cacheing.CacheServicesFactory;

namespace Trainova.Infrastructure.Cacheing
{
    public class CacheServiceBridge<TValue> : ICacheService<TValue>
    {
        private readonly ICacheService<TValue> _innerCacheService;

        public CacheServiceBridge(CacheServiceFactory factory)
        {
            _innerCacheService = factory.GetCacheService<TValue>();
        }

        public Task<TValue?> GetAsync(string cacheKey)
        {
            return _innerCacheService.GetAsync(cacheKey);
        }

        public Task SetAsync<TValue>(string cacheKey, TValue value, TimeSpan? expiration)
        {
            return _innerCacheService.SetAsync(cacheKey, value, expiration);
        }
    }
}
