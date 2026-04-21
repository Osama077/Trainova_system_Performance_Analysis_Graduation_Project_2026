using Microsoft.OpenApi;
using Trainova.Api.Services;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Application.Common.Models;

namespace Trainova.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
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

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Trainova API",
                Version = "v1"
            });

            options.AddSecurityDefinition("Bearer",
                new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter JWT token"
                });

            options.AddSecurityRequirement(document =>
            {
                var requirement = new OpenApiSecurityRequirement();

                requirement.Add(
                    new OpenApiSecuritySchemeReference(
                        "Bearer",
                        document,
                        null),
                    new List<string>());

                return requirement;
            });
        });






        services.AddOpenApi();

        return services;
    }
}