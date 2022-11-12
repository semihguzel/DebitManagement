using DebitManagement.Service.Debit.Mappings;
using DebitManagement.Service.Product.Mappings;
using DebitManagement.Service.ProductType.Mappings;

namespace DebitManagement.API.Extensions;

public static class MapperProfileServicesExtensions
{
    public static IServiceCollection AddMapperProfiles(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ProductTypeProfiles));
        services.AddAutoMapper(typeof(ProductProfiles));
        services.AddAutoMapper(typeof(DebitProfiles));

        return services;
    }
}