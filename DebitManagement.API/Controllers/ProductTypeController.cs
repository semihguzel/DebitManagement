using System.Net;
using AutoMapper;
using DebitManagement.Base;
using DebitManagement.Core.Entities;
using DebitManagement.Core.Interfaces;
using DebitManagement.Service.ProductType;
using DebitManagement.Service.ProductType.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DebitManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductTypeController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IProductTypeRepository _productTypeRepository;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly ProductTypeService _productTypeService;

    public ProductTypeController(IUserRepository userRepository, IProductRepository productRepository,
        IProductTypeRepository productTypeRepository, IConfiguration configuration, IMapper mapper)
    {
        _userRepository = userRepository;
        _productTypeRepository = productTypeRepository;
        _configuration = configuration;
        _mapper = mapper;
        _productTypeService = new ProductTypeService(configuration, productTypeRepository, mapper);
    }

    [HttpGet]
    public async Task<ActionResult> GetProductTypes()
    {
        IReadOnlyList<ProductType> list;
        try
        {
            list = await _productTypeRepository.GetAllAsync();
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
            new ResponseBody { Items = list.ToList<object>(), Count = list.Count });
    }

    [HttpPost("Create"), Authorize(Roles = "Admin")]
    public async Task<ActionResult> CreateProductType(ProductTypeDto productTypeDto)
    {
        try
        {
            ProductTypeHelper.CheckSentData(productTypeDto);
            await _productTypeService.CheckIfAlreadyExists(productTypeDto.ProductTypeCode);
            await _productTypeService.Insert(productTypeDto);
        }
        catch (HttpException e)
        {
            return StatusCode((int)e.StatusCode, new ResponseBody { Message = e.ErrorMessage });
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        return StatusCode((int)HttpStatusCode.Created, "Successfully created.");
    }

    [HttpPost("Delete"), Authorize(Roles = "Admin")]
    public async Task<ActionResult> DeleteProductType(string productTypeCode)
    {
        try
        {
            if (string.IsNullOrEmpty(productTypeCode))
                throw new HttpException(HttpStatusCode.NotAcceptable,
                    "Product type code cannot be empty. Please check sent data.");

            var roleType = await _productTypeService.CheckAndReturn(productTypeCode);
            await _productTypeRepository.Delete(roleType.Id);
        }
        catch (HttpException e)
        {
            return StatusCode((int)e.StatusCode, new ResponseBody { Message = e.ErrorMessage });
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        return StatusCode((int)HttpStatusCode.OK, "Successfully deleted.");
    }

    [HttpPost("Update"), Authorize(Roles = "Admin")]
    public async Task<ActionResult> UpdateProductType(Guid id, ProductTypeDto productTypeDto)
    {
        try
        {
            var productType = await _productTypeService.CheckByIdAndReturn(id);

            var updatedProductType = ProductTypeHelper.UpdateModel(productType, productTypeDto);
            await _productTypeRepository.Update(updatedProductType);
        }
        catch (HttpException e)
        {
            return StatusCode((int)e.StatusCode, new ResponseBody { Message = e.ErrorMessage });
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        return StatusCode((int)HttpStatusCode.OK, "Successfully updated.");
    }
}