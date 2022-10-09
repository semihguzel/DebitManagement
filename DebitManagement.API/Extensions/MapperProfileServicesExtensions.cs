using DebitManagement.Service.ProductType.Mappings;

namespace DebitManagement.API.Extensions;

public static class MapperProfileServicesExtensions
{
    public static IServiceCollection AddMapperProfiles(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ProductTypeProfiles));

        return services;
    }
}