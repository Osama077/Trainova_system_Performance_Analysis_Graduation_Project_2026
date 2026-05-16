using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Trainova.Application.Common.Behaviors;
using Trainova.Application.Common.Models;

namespace Trainova.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));
        });
        services.AddTransient(typeof(MediatR.IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddTransient(typeof(MediatR.IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));

        services.AddSingleton<EFCoreLoggingOptions>(configuration.GetSection("EFCoreLoggingOptions").Get<EFCoreLoggingOptions>());

        services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection));

        return services;
    }
}