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
            return StatusCode((int)e.StatusCode, new ResponseBody<Product> { Message = e.ErrorMessage });
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        return StatusCode((int)HttpStatusCode.OK,
            new ResponseBody<Product> { Items = products.ToList<object>(), Count = products.Count });
    }

    [HttpPost, Authorize(Roles = "Admin")]
    public async Task<ActionResult> CreateProduct(ProductDto productDto)
    {
        Product createdProduct = null;
        try
        {
            ProductHelper.CheckSentData(productDto);
            await _productService.CheckIfAlreadyExists(productDto.ProductCode);
            createdProduct = await _productService.Insert(productDto);
        }
        catch (HttpException e)
        {
            return StatusCode((int)e.StatusCode, new ResponseBody<Product> { Message = e.ErrorMessage });
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        return StatusCode((int)HttpStatusCode.OK, new ResponseBody<Product> { Item = createdProduct });
    }

    [HttpPost("Delete"), Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteProduct(string productCode)
    {
        try
        {
            if (string.IsNullOrEmpty(productCode))
                throw new HttpException(HttpStatusCode.NotAcceptable,
                    "Product code cannot be empty. Please check sent data.");

            var product = await _productService.CheckByCodeAndReturn(productCode);
            await _productRepository.Delete(product.Id);
        }
        catch (HttpException e)
        {
            return StatusCode((int)e.StatusCode, new ResponseBody<Product> { Message = e.ErrorMessage });
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        return StatusCode((int)HttpStatusCode.OK, "Successfully deleted.");
    }

    [HttpPost("Update"), Authorize(Roles = "Admin")]
    public async Task<ActionResult> UpdateProduct(Guid id, ProductDto productDto)
    {
        try
        {
            var product = await _productService.CheckByIdAndReturn(id);
            var updatedProduct = ProductHelper.UpdateModel(product, productDto);
            await _productRepository.Update(updatedProduct);
        }
        catch (HttpException e)
        {
            return StatusCode((int)e.StatusCode, new ResponseBody<Product> { Message = e.ErrorMessage });
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        return StatusCode((int)HttpStatusCode.OK, "Successfully updated.");
    }
}