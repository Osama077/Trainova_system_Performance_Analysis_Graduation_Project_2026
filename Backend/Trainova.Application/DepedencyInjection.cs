using Trainova.Application.Common.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Trainova.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));
        });
        services.AddTransient(typeof(MediatR.IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddTransient(typeof(MediatR.IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));



        services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection));

        return services;
    }
}