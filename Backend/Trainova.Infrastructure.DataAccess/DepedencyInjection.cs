using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Trainova.Application.Common.Interfaces.Repositories;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Infrastructure.DataAccess.DbSettingsObjects;
using Trainova.Infrastructure.DataAccess.Repositories.Outbox;
using Trainova.Infrastructure.DataAccess.Repositories.Users;


namespace Trainova.Infrastructure.DataAccess;

public static class DependencyInjection
{


    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<TrainovaWriteDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("TrainovaWriteDbConnection");
            options.UseSqlServer(connectionString);
        });
        services.AddScoped<IUnitOfWork>(sp =>
            sp.GetRequiredService<TrainovaWriteDbContext>());

        services.AddSingleton<IDbSettings>(new DbSettings(
                configuration.GetConnectionString("TrainovaWriteDbConnection"),
                configuration.GetConnectionString("TrainovaReadDbConnection"))
        );


        //User Related Repos
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IRolesRepository, RolesRepository>();
        services.AddScoped<IUserTokensRepository, UserTokensRepository>();
        services.AddScoped<IUserRolesRepository, UserRolesRepository>();




        // Outbox
        services.AddScoped<IEmailOutboxRepository, EmailOutboxRepository>();


        // Register Dapper type handlers for enums
        DapperTypeHandlerConfiguration.Register();





        return services;
    }




}