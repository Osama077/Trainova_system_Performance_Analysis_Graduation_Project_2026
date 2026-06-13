using Microsoft.Extensions.DependencyInjection;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Infrastructure.Cacheing.strategies;

namespace Trainova.Infrastructure.Cacheing.CacheServicesFactory
{
    public class CacheServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TargetedCacheType _cacheType;

        public CacheServiceFactory(IServiceProvider serviceProvider, TargetedCacheType cacheType)
        {
            _serviceProvider = serviceProvider;
            _cacheType = cacheType;
        }

        public ICacheService<TValue> GetCacheService<TValue>()
        {
            return _cacheType switch
            {
                TargetedCacheType.InMemory => _serviceProvider.GetRequiredService<InMemoryCacheStrategy<TValue>>(),
                TargetedCacheType.Redis => _serviceProvider.GetRequiredService<RedisCacheStrategy<TValue>>(),
                _ => throw new NotImplementedException($"Cache type {_cacheType} is not supported.")
            };
        }
    }

}
