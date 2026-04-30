using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Trainova.Application.Common.Interfaces.Repositories.CommonRepos;
using Trainova.Application.Common.Interfaces.Repositories.MedicalStatus;
using Trainova.Application.Common.Interfaces.Repositories.Profiles.Players;
using Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity;
using Trainova.Application.Common.Interfaces.Repositories.UserAuth;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Infrastructure.DataAccess.DbSettingsObjects;
using Trainova.Infrastructure.DataAccess.Repositories.Common;
using Trainova.Infrastructure.DataAccess.Repositories.MedicalStatus;
using Trainova.Infrastructure.DataAccess.Repositories.Profiles;
using Trainova.Infrastructure.DataAccess.Repositories.TrainingSessionAccessablity;
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


        //User and Auth Related Repos
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IRolesRepository, RolesRepository>();
        services.AddScoped<IUserTokensRepository, UserTokensRepository>();
        services.AddScoped<IUserRolesRepository, UserRolesRepository>();

        //medical
        services.AddScoped<IInjuryRepository, InjuryRepository>();
        services.AddScoped<IPlayerInjuryRepository, PlayerInjuryRepository>();
        services.AddScoped<IRecoveryPlanPhasesRepository, RecoveryPlanPhasesRepository>();
        // Audit
        services.AddScoped<IAuditRepository,AuditRepository>();
        // Outbox
        services.AddScoped<IEmailOutboxRepository, EmailOutboxRepository>();


        // Profiles
        services.AddScoped<IPlayerRepository,PlayerRepository>();
        // Register Dapper type handlers for enums
        DapperTypeHandlerConfiguration.Register();


        // TrainingSessionAccessablity

        services.AddScoped<ITrainingSessionRepository, TrainingSessionRepository>();
        services.AddScoped<IAccsessPolicyRepository, AccsessPolicyRepository>();
        services.AddScoped<IUserAccessPolicyRepository, UserAccessPolicyRepository>();
        services.AddScoped<IPlanRepository, PlanRepository>();

        return services;
    }




}