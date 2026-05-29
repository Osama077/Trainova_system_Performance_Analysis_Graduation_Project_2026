using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Trainova.Application.Common.Interfaces.Repositories.CommonRepos;
using Trainova.Application.Common.Interfaces.Repositories.FitnessStatus;
using Trainova.Application.Common.Interfaces.Repositories.MedicalStatus;
using Trainova.Application.Common.Interfaces.Repositories.Profiles.Players;
using Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity;
using Trainova.Application.Common.Interfaces.Repositories.UserAuth;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Infrastructure.DataAccess.DbSettingsObjects;
using Trainova.Infrastructure.DataAccess.Repositories.Common;
using Trainova.Infrastructure.DataAccess.Repositories.FitnessStatus;
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

        services.AddSingleton<ConnectionString>(sp =>
        {
            var connStringObj = configuration.GetSection("ConnectionStrings").Get<ConnectionString>();

            if (connStringObj == null || string.IsNullOrEmpty(connStringObj.TrainovaWriteDbConnection))
            {
                throw new InvalidOperationException("Could not load connection strings from configuration.");
            }

            return connStringObj;
        });


        //services.AddSingleton<IDbSettings>(new DbSettings(
        //        configuration.GetConnectionString("TrainovaWriteDbConnection"),
        //        configuration.GetConnectionString("TrainovaReadDbConnection"))
        //);




        services.AddScoped<IDbSettings, DbSettings>();

        //User and Auth Related Repos
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IUserTokensRepository, UserTokensRepository>();

        //medical
        services.AddScoped<IInjuryRepository, InjuryRepository>();
        services.AddScoped<IPlayerInjuryRepository, PlayerInjuryRepository>();
        // Audit
        services.AddScoped<IAuditRepository, AuditRepository>();
        // Outbox
        services.AddScoped<IEmailOutboxRepository, EmailOutboxRepository>();
        services.AddScoped<IEventOutboxRepository, EventOutboxRepository>();

        // Profiles
        services.AddScoped<IPlayerRepository, PlayerRepository>();
        services.AddScoped<Trainova.Application.Common.Interfaces.Repositories.Profiles.Candidates.ICandidateRepository, Trainova.Infrastructure.DataAccess.Repositories.Profiles.CandidateRepository>();
        // Register Dapper type handlers for enums
        DapperTypeHandlerConfiguration.Register();


        // TrainingSessionAccessablity

        services.AddScoped<ITrainingSessionRepository, TrainingSessionRepository>();
        services.AddScoped<Trainova.Application.Common.Interfaces.Repositories.MatchsManagement.Matches.ICandidateMatchRepository, Trainova.Infrastructure.DataAccess.Repositories.MatchsManagement.Matches.CandidateMatchRepository>();
        services.AddScoped<IAccessPolicyRepository, AccsessPolicyRepository>();
        services.AddScoped<IUserAccessPolicyRepository, UserAccessPolicyRepository>();
        services.AddScoped<IPlanRepository, PlanRepository>();

        // FitnessStatus
        services.AddScoped<IFitnessExerciseRepository, FitnessExerciseRepository>();
        services.AddScoped<IFitnessSessionExerciseRepository, FitnessSessionExerciseRepository>();
        services.AddScoped<IPhysicalCapacityTestRepository, PhysicalCapacityTestRepository>();
        services.AddScoped<ISessionMovementRepository, SessionMovementRepository>();



        return services;
    }




}