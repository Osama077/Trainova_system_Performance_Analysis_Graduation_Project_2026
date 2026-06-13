using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Infrastructure.Cacheing.CacheServicesFactory;
using Trainova.Infrastructure.Cacheing.strategies;

namespace Trainova.Infrastructure.Cacheing
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCaching(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();

            var cacheTypeString = configuration["CacheSettings:CacheType"]
                ?? throw new InvalidOperationException("CacheType is missing in configuration.");

            var cacheType = Enum.Parse<TargetedCacheType>(cacheTypeString);




            //========================================================

            cacheType = TargetedCacheType.InMemory; // Force in-memory cache for development and testing

            //========================================================


            services.AddTransient(typeof(InMemoryCacheStrategy<>));
            services.AddTransient(typeof(RedisCacheStrategy<>));

            if (cacheType == TargetedCacheType.Redis)
            {
                var redisOptions = configuration.GetSection("CacheSettings:RedisOptions").Get<CacheIntegrationOptions>()
                    ?? throw new InvalidOperationException("Redis configuration options are missing.");

                services.AddSingleton(redisOptions);

                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = redisOptions.RedisConnectionString;
                    options.InstanceName = "Trainova_";
                });
            }
            else
            {
                services.AddDistributedMemoryCache();
            }

            services.AddSingleton(sp => new CacheServiceFactory(sp, cacheType));

            // Fixes the Open Generic factory exception by using a generic forwarding registration
            services.AddTransient(typeof(ICacheService<>), typeof(CacheServiceBridge<>));

            return services;
        }
    }
}
