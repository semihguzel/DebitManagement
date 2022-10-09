using AutoMapper;
using DebitManagement.Service.Product.Dto;

namespace DebitManagement.Service.Product.Mappings;

public class ProductProfiles : Profile
{
    public ProductProfiles()
    {
        CreateMap<Core.Entities.Product, ProductDto>();
        CreateMap<ProductDto, Core.Entities.Product>();
    }
}