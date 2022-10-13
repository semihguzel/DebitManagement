using System.Net;
using AutoMapper;
using DebitManagement.API.Controllers;
using DebitManagement.Base;
using DebitManagement.Core.Entities;
using DebitManagement.Core.Interfaces;
using DebitManagement.Service.Product;
using Moq;

namespace DebitManagement.Tests.ProductTests;

public class ProductServiceTests
{
    private readonly Mock<IProductRepository> _mockProductRepository;
    private readonly Mock<IProductTypeRepository> _mockProductTypeRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly ProductService _service;

    public ProductServiceTests()
    {
        _mockProductRepository = new Mock<IProductRepository>();
        _mockProductTypeRepository = new Mock<IProductTypeRepository>();
        _mockMapper = new Mock<IMapper>();
        _service = new ProductService(_mockProductRepository.Object, _mockProductTypeRepository.Object,
            _mockMapper.Object);
    }

    [Test]
    public async Task CheckIfExists()
    {
        var initialProduct = new Product()
        {
            Id = new Guid("69bb25c4-03bf-4495-a52f-1c9e47487872"),
            ProductCode = "macbook",
            ProductDescription = "macbook pro 14 inch",
            Price = 50
        };
        _mockProductRepository.Setup(repo => repo.GetByCode(It.Is<string>(x => x == initialProduct.ProductCode)))
            .ReturnsAsync(initialProduct);

        var product = await _mockProductRepository.Object.GetByCode("macbook");

        if (product == null)
            throw new HttpException(HttpStatusCode.NotAcceptable, "Product does not exists. Please check sent data");

        Assert.NotNull(product);
    }
}