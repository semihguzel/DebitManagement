using System.Net;
using AutoMapper;
using DebitManagement.Base;
using DebitManagement.Core.Entities;
using DebitManagement.Core.Interfaces;
using DebitManagement.Service.Product;
using DebitManagement.Service.Product.Dto;
using DebitManagement.Service.Product.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DebitManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly IProductTypeRepository _productTypeRepository;
    private readonly IMapper _mapper;

    private ProductService _productService;

    public ProductController(IProductRepository productRepository, IProductTypeRepository productTypeRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _productTypeRepository = productTypeRepository;
        _mapper = mapper;
        _productService = new ProductService(productRepository, productTypeRepository, mapper);
    }

    [HttpGet]
    public async Task<ActionResult> GetProducts()
    {
        IReadOnlyList<Product> products;
        try
        {
            products = await _productRepository.GetAllAsync();
        }
        catch (HttpException e)
        {
            return StatusCode((int)e.StatusCode, new ResponseBody { Message = e.ErrorMessage });
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        return StatusCode((int)HttpStatusCode.OK,
            new ResponseBody { Items = products.ToList<object>(), Count = products.Count });
    }

    [HttpPost, Authorize(Roles = "Admin")]
    public async Task<ActionResult> CreateProduct(ProductDto productDto)
    {
        try
        {
            ProductHelper.CheckSentData(productDto);
            await _productService.CheckIfAlreadyExists(productDto.ProductCode);
            await _productService.Insert(productDto);
        }
        catch (HttpException e)
        {
            return StatusCode((int)e.StatusCode, new ResponseBody { Message = e.ErrorMessage });
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
        
        return StatusCode((int)HttpStatusCode.OK, "Successfully created.");
    }
}