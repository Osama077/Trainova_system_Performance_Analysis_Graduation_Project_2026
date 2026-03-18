using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Trainova.Api.Services;
using Trainova.Application.Common.Interfaces.Repositories.CommonRepos;
using Trainova.Application.Common.Interfaces.Repositories.UserAuth;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Application.Common.Models;
using Trainova.Infrastructure.DataAccess;
using Trainova.Infrastructure.DataAccess.DbSettingsObjects;
using Trainova.Infrastructure.DataAccess.Repositories.Outbox;
using Trainova.Infrastructure.DataAccess.Repositories.Users;


namespace Trainova.Api;

public static class DependencyInjection
{


    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();
        services.AddScoped<CurrentUser>(sp =>
        {
            CurrentUser? currentUser = sp.GetRequiredService<ICurrentUserProvider>().GetCurrentUser();
            return currentUser;
        });
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddOpenApi();



        return services;
    }




}