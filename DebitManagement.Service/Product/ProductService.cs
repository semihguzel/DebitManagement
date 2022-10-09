using System.Net;
using AutoMapper;
using DebitManagement.Base;
using DebitManagement.Core.Interfaces;
using DebitManagement.Service.Product.Dto;

namespace DebitManagement.Service.Product;

public class ProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IProductTypeRepository _productTypeRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IProductTypeRepository productTypeRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _productTypeRepository = productTypeRepository;
        _mapper = mapper;
    }

    public async Task CheckIfAlreadyExists(string productCode)
    {
        var product = await _productRepository.GetByCode(productCode);

        if (product != null)
            throw new HttpException(HttpStatusCode.NotAcceptable, "Product already exists, please check sent data");
    }

    public async Task Insert(ProductDto productDto)
    {
        var product = _mapper.Map<Core.Entities.Product>(productDto);
        await _productRepository.Create(product);
    }
}