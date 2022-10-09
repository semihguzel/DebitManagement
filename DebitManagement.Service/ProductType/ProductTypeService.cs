using System.Net;
using AutoMapper;
using DebitManagement.Base;
using DebitManagement.Core.Interfaces;
using DebitManagement.Service.ProductType.Dto;
using Microsoft.Extensions.Configuration;

namespace DebitManagement.Service.ProductType;

public class ProductTypeService
{
    private readonly IConfiguration _configuration;
    private readonly IProductTypeRepository _productTypeRepository;
    private readonly IMapper _mapper;

    public ProductTypeService(IConfiguration configuration, IProductTypeRepository productTypeRepository,
        IMapper mapper)
    {
        _configuration = configuration;
        _productTypeRepository = productTypeRepository;
        _mapper = mapper;
    }

    public async Task CheckIfAlreadyExists(string productTypeCode)
    {
        var productType = await _productTypeRepository.GetByCode(productTypeCode);

        if (productType != null)
            throw new HttpException(HttpStatusCode.NotAcceptable, "Role type already exists, please check sent data");
    }

    public async Task<Core.Entities.ProductType> CheckAndReturn(string productTypeCode)
    {
        var productType = await _productTypeRepository.GetByCode(productTypeCode);

        if (productType == null)
            throw new HttpException(HttpStatusCode.NotAcceptable,
                "Role type couldn't be found, please sent check data");

        return productType;
    }
    
    public async Task<Core.Entities.ProductType> CheckByIdAndReturn(Guid id)
    {
        var productType = await _productTypeRepository.GetByIdAsync(id);

        if (productType == null)
            throw new HttpException(HttpStatusCode.NotAcceptable,
                "Role type couldn't be found, please sent check data");

        return productType;
    }

    public async Task Insert(ProductTypeDto productTypeDto)
    {
        var entity = _mapper.Map<Core.Entities.ProductType>(productTypeDto);
        await _productTypeRepository.Create(entity);
    }
}