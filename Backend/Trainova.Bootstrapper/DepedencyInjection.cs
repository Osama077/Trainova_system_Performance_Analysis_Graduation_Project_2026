using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Trainova.Infrastructure.Authorization;
using Trainova.Infrastructure.DataAccess;


namespace Trainova.Bootstrapper;

public static class DependencyInjection
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddPersistence(configuration)
            .AddAuthentication(configuration);
    }



}