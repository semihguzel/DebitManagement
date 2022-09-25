using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace DebitManagement.API.Extensions;

public static class SwaggerServicesExtensions
{
    public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("CoreSwagger", new OpenApiInfo()
            {
                Title = "Swagger on ASP.NET Core",
                Version = "1.0.0",
                Description = "Try Swagger on (ASP.NET Core 2.1)"
            });

            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Description = "Standard Authorization header using the Bearer scheme",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            
            options.OperationFilter<SecurityRequirementsOperationFilter>();
        });


        return services;
    }
}