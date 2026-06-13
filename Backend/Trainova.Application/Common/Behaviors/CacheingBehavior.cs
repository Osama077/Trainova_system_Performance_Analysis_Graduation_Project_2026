using MediatR;
using System.Reflection;
using System.Text;
using Trainova.Application.Common.Cacheing;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Common.Behaviors
{
    public class CacheingBehavior<TRequest, TResponse, TValue> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ICacheableQuery
        where TResponse : IResultOf<TValue>
    {
        private readonly ICacheService<TValue> _cacheService;

        public CacheingBehavior(ICacheService<TValue> cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {

            string cacheKey = GenerateCacheKey(request);

            TValue? cachedValue = await _cacheService.GetAsync(cacheKey);

            if (cachedValue is not null)
            {
                return (dynamic)ResultOf<TValue>.Cached(cachedValue);
            }

            var response = await next();

            if (response.IsFailure)
            {
                return response;
            }

            await _cacheService.SetAsync(cacheKey, response.Value, request.Expiration ?? TimeSpan.FromHours(1));

            return response;
        }

        private static string GenerateCacheKey(TRequest request)
        {
            var sb = new StringBuilder();
            var requestType = typeof(TRequest);

            var requestTypeName = requestType.Name;

            sb.Append(request.CacheKeyPrefix ?? requestTypeName);

            var propertiesWithAttribute = requestType
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Select(p => new
                {
                    Property = p,
                    Attribute = p.GetCustomAttribute<CacheKeyParameterAttribute>()
                })
                .Where(x => x.Attribute != null)
                .OrderBy(x => x.Property.Name);

            foreach (var item in propertiesWithAttribute)
            {
                var value = item.Property.GetValue(request);
                if (value is not null)
                {
                    string keyName = !string.IsNullOrWhiteSpace(item.Attribute!.KeyName)
                        ? item.Attribute.KeyName
                        : item.Property.Name;

                    sb.Append($"_{keyName}:{value}");
                }
            }

            return sb.ToString();
        }
    }
}