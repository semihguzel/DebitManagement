using System.Net;
using AutoMapper;
using DebitManagement.Base;
using DebitManagement.Base.Debit;
using DebitManagement.Core.Entities;
using DebitManagement.Core.Interfaces;
using DebitManagement.Service.Debit;
using DebitManagement.Service.Debit.Dto;
using DebitManagement.Service.Debit.Helpers;
using DebitManagement.Service.Product;
using DebitManagement.Service.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DebitManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DebitController : ControllerBase
{
    private readonly IDebitRepository _debitRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;
    private readonly IProductTypeRepository _productTypeRepository;
    private readonly IDebitActionHistoryRepository _debitActionHistoryRepository;
    private readonly DebitService _debitService;
    private readonly UserService _userService;
    private readonly ProductService _productService;

    public DebitController(IDebitRepository debitRepository, IUserRepository userRepository, IMapper mapper,
        IProductRepository productRepository, IProductTypeRepository productTypeRepository,
        IDebitActionHistoryRepository debitActionHistoryRepository)
    {
        _debitRepository = debitRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _productRepository = productRepository;
        _productTypeRepository = productTypeRepository;
        _debitActionHistoryRepository = debitActionHistoryRepository;
        _debitService = new DebitService(debitRepository, mapper, productRepository, productTypeRepository);
        _userService = new UserService(userRepository, mapper);
        _productService = new ProductService(productRepository, productTypeRepository, mapper);
    }

    [HttpGet("GetUserDebits")]
    [Authorize(Roles = "User")]
    public async Task<ActionResult> GetUserDebits(string username)
    {
        List<DebitDto> debitList = null;
        try
        {
            await _userService.CheckUserAndReturn(username);
            var userDebits = await _debitRepository.GetUserDebits(username);
            debitList = _mapper.Map<List<DebitDto>>(userDebits);
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
            new ResponseBody<Debit> { Items = debitList.ToList<object>(), Count = debitList.Count });
    }

    [HttpPost("CreateOrUpdateDebit")]
    [Authorize(Roles = "Manager")]
    public async Task<ActionResult> CreateOrUpdateDebit(DebitCreateUpdateDto createUpdateDebitDto)
    {
        Debit createdDebit = null;
        bool isUpdated = false;
        try
        {
            DebitHelper.CheckSentData(createUpdateDebitDto);

            var product = await _productService.CheckByIdAndReturn(createUpdateDebitDto.ProductId);
            var user = await _userService.CheckByIdAndReturn(createUpdateDebitDto.UserId);

            await _debitService.CheckProductQuantity(product.ProductCode, createUpdateDebitDto.Quantity);
            var debit = await _debitService.ReturnIfUserAlreadyHave(user.Username, product.ProductCode);

            if (debit != null)
            {
                isUpdated = true;
                debit.Quantity += createUpdateDebitDto.Quantity;
                await _debitActionHistoryRepository.Create(new DebitActionHistory
                {
                    Action = DebitActionConstants.Update,
                    DebitId = debit.Id,
                    ActionDescription = "Quantity updated"
                });
                await _debitRepository.Update(debit);
            }
            else
            {
                createdDebit = new Debit();
                await _debitActionHistoryRepository.Create(new DebitActionHistory
                {
                    Action = DebitActionConstants.Create,
                    DebitId = debit.Id,
                    ActionDescription = "Debit created."
                });
                createdDebit = DebitHelper.UpdateModel(createdDebit, createUpdateDebitDto);
                await _debitRepository.Create(createdDebit);

                product.Quantity = Math.Abs(product.Quantity - createUpdateDebitDto.Quantity);

                await _productRepository.Update(product);
            }
        }
        catch (HttpException e)
        {
            return StatusCode((int)e.StatusCode, new ResponseBody<Debit> { Message = e.ErrorMessage });
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        if (!isUpdated)
            return StatusCode((int)HttpStatusCode.Created,
                new ResponseBody<Debit> { Item = createdDebit, Message = "Successfully created." });

        return StatusCode((int)HttpStatusCode.OK,
            new ResponseBody<Debit> { Item = createdDebit, Message = "Quantity successfully updated." });
    }

    [HttpPost("RecoverDebit")]
    [Authorize(Roles = "Manager")]
    public async Task<ActionResult> RecoverDebit(Guid debitId)
    {
        try
        {
            if (debitId == Guid.Empty)
                throw new HttpException(HttpStatusCode.NotAcceptable,
                    "DebitId cannot be empty. Please check it and try again.");

            var debit = await _debitRepository.GetEntityById(debitId);

            if (debit == null)
                throw new HttpException(HttpStatusCode.NotAcceptable,
                    "Cannot find debit. Please check it and try again.");

            debit.Status = DebitStatusConstants.InActive;

            await _debitActionHistoryRepository.Create(new DebitActionHistory
            {
                DebitId = debit.Id,
                Action = DebitActionConstants.InActive,
                ActionDescription = "Debit recovered."
            });

            await _debitRepository.Update(debit);

            debit.Product.Quantity = Math.Abs(debit.Product.Quantity + debit.Quantity);

            await _productRepository.Update(debit.Product);
        }
        catch (HttpException e)
        {
            return StatusCode((int)e.StatusCode, new ResponseBody<Debit> { Message = e.ErrorMessage });
        }
        catch (Exception e)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        return StatusCode((int)HttpStatusCode.OK,
            new ResponseBody<Debit> { Message = "Quantity successfully recovered." });
    }
}