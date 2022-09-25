using DebitManagement.Data.Interfaces;
using DebitManagement.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DebitManagement.API.Extendions;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

        return services;
    }
}