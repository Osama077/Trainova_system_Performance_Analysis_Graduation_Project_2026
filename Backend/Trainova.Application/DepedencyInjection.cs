using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Trainova.Application.Common.Behaviors;
using Trainova.Application.Common.Cacheing;
using Trainova.Application.Common.Models;
using Trainova.Common.ResultOf;

namespace Trainova.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));
        });
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
        //services.AddTransient(typeof(MediatR.IPipelineBehavior<,>), typeof(CacheingBehavior<,,>));
        services.AddCachingBehavior();

        services.AddSingleton<LoggingOptions>(configuration.GetSection("LoggingOptions").Get<LoggingOptions>());

        services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection));

        return services;
    }

    private static IServiceCollection AddCachingBehavior(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        var cacheableQueryTypes = assembly.GetTypes()
            .Where(t => !t.IsInterface && !t.IsAbstract)
            .Select(t => new
            {
                QueryType = t,
                RequestInterface = t.GetInterfaces()
                    .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequest<>))
            })
            .Where(x => x.RequestInterface != null && typeof(ICacheableQuery).IsAssignableFrom(x.QueryType))
            .ToList();

        foreach (var item in cacheableQueryTypes)
        {
            var requestType = item.QueryType;
            var responseType = item.RequestInterface!.GetGenericArguments()[0];

            var resultOfInterface = responseType.GetInterfaces()
                .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IResultOf<>));

            if (resultOfInterface != null)
            {
                var tValue = resultOfInterface.GetGenericArguments()[0];

                var closedBehaviorType = typeof(CacheingBehavior<,,>).MakeGenericType(requestType, responseType, tValue);

                var closedPipelineInterface = typeof(IPipelineBehavior<,>).MakeGenericType(requestType, responseType);

                services.AddTransient(closedPipelineInterface, closedBehaviorType);
            }
        }

        return services;
    }
}