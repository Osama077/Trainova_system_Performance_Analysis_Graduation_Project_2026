using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.OpenApi;
using Trainova.Api.Filters;
using Trainova.Api.Services;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Application.Common.Models;
using Trainova.Domain.Common.DataConvrters;

namespace Trainova.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();
        services.AddScoped<CurrentUser>(sp => sp.GetRequiredService<ICurrentUserProvider>().GetCurrentUser());

        services.AddCors(options =>
        {
            options.AddPolicy("DefaultCorsPolicy", policy =>
            {
                policy
                    .SetIsOriginAllowed(_ => true)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        services.AddControllers()
            .AddJsonOptions(options =>
                    options.JsonSerializerOptions.Converters.Add(new NonFlagsEnumConverterFactory())
            );

        services.AddEndpointsApiExplorer();

        services.AddScoped<IdempotencyFilter>();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Trainova API",
                Version = "v2"
            });


            options.OperationFilter<IdempotencyHeaderFilter>();




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

        services.AddMemoryCache();
        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders =
                ForwardedHeaders.XForwardedFor |
                ForwardedHeaders.XForwardedProto;

            options.KnownIPNetworks.Clear();
            options.KnownProxies.Clear();
        });



        services.AddOpenApi();

        return services;
    }

}