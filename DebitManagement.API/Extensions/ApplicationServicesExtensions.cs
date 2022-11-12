using DebitManagement.Core.Interfaces;
using DebitManagement.Repository.Repositories;

namespace DebitManagement.API.Extensions;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
        services.AddScoped<IDebitRepository, DebitRepository>();
        services.AddScoped<IDebitActionHistoryRepository, DebitActionHistoryRepository>();
        

        return services;
    }
}