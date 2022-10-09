using AutoMapper;
using DebitManagement.Service.ProductType.Dto;

namespace DebitManagement.Service.ProductType.Mappings;

public class ProductTypeProfiles : Profile
{
    public ProductTypeProfiles()
    {
        CreateMap<Core.Entities.ProductType, ProductTypeDto>();
        CreateMap<ProductTypeDto, Core.Entities.ProductType>();
    }
}