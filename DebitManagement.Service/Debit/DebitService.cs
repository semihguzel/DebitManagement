using System.Net;
using AutoMapper;
using DebitManagement.Base;
using DebitManagement.Core.Interfaces;
using DebitManagement.Service.Product;
using DebitManagement.Service.User;

namespace DebitManagement.Service.Debit;

public class DebitService
{
    private readonly IDebitRepository _debitRepository;
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;
    private readonly ProductService _productService;

    public DebitService(IDebitRepository debitRepository, IMapper mapper, IProductRepository productRepository,
        IProductTypeRepository productTypeRepository)
    {
        _debitRepository = debitRepository;
        _mapper = mapper;
        _productRepository = productRepository;
        _productService = new ProductService(productRepository, productTypeRepository, mapper);
    }

    public async Task CheckProductQuantity(string productCode, int debitQuantity)
    {
        var product = await _productService.CheckByCodeAndReturn(productCode);

        if (product.Quantity < debitQuantity)
            throw new HttpException(HttpStatusCode.NotAcceptable,
                $"There are only {product.Quantity} amount of items on the database. Please lower quantity and try again");
    }

    public async Task<Core.Entities.Debit?> ReturnIfUserAlreadyHave(string username, string productCode)
    {
        var userDebits = await _debitRepository.GetUserDebits(username);

        var userDebitProduct =
            userDebits.FirstOrDefault(x => x.Product.ProductCode == productCode && x.Status == "Active");

        return userDebitProduct;
    }
    
}