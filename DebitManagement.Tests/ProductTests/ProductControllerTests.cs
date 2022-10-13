using AutoMapper;
using DebitManagement.API.Controllers;
using DebitManagement.Core.Entities;
using DebitManagement.Core.Interfaces;
using Moq;

namespace DebitManagement.Tests.ProductTests;

public class ProductControllerTests
{
    private readonly Mock<IProductRepository> _mockProductRepository;
    private readonly Mock<IProductTypeRepository> _mockProductTypeRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _mockProductRepository = new Mock<IProductRepository>();
        _mockProductTypeRepository = new Mock<IProductTypeRepository>();
        _mockMapper = new Mock<IMapper>();
        _controller = new ProductController(_mockProductRepository.Object, _mockProductTypeRepository.Object,
            _mockMapper.Object);
    }

    [Test]
    public async Task ShouldDeleteProduct()
    {
        var initialProduct = new Product()
        {
            Id = new Guid("69bb25c4-03bf-4495-a52f-1c9e47487872"),
            ProductCode = "macbook",
            ProductDescription = "macbook pro 14 inch",
            Price = 50
        };

        _mockProductRepository.Setup(x => x.Delete(It.Is<Guid>(y => y == initialProduct.Id)));

        await _mockProductRepository.Object.Delete(initialProduct.Id);
        
    }
}