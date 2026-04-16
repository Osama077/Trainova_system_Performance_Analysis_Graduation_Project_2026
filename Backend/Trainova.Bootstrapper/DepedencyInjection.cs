using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Trainova.Application.Common.Interfaces.Service;
using Trainova.Bootstrapper.BackgroundServises;
using Trainova.Bootstrapper.Helpers;
using Trainova.Bootstrapper.Services;
using Trainova.Infrastructure.Authorization;
using Trainova.Infrastructure.DataAccess;


namespace Trainova.Bootstrapper;

public static class DependencyInjection
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddPersistence(configuration)
            .AddMailService(configuration)
            .AddAuthentication(configuration);
    }

    private static IServiceCollection AddMailService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddHostedService<EmailOutboxBackGroundService>();
        return services;
    }

}