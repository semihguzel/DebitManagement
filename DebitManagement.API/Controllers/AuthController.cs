using System.Net;
using DebitManagement.API.Dto;
using DebitManagement.Base;
using DebitManagement.Base.Auth;
using DebitManagement.Data.Entities;
using DebitManagement.Data.Interfaces;
using DebitManagement.Service.Auth;
using Microsoft.AspNetCore.Mvc;

namespace DebitManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _repository;
    private readonly AuthService _authService;

    public AuthController(IUserRepository repository)
    {
        _repository = repository;
        _authService = new AuthService(_repository);
    }

    [HttpPost("Register")]
    public async Task<ActionResult> Register([FromBody] UserDto request)
    {
        try
        {
            AuthHelper.ValidateRequest(request.Username, request.Password);
            await _authService.CheckForRegister(request.Username);

            AuthHelper.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User()
                { Username = request.Username, PasswordHash = passwordHash, PasswordSalt = passwordSalt };

            await _repository.Create(user);
        }
        catch (HttpException e)
        {
            return StatusCode((int)e.StatusCode, new ResponseBody { Message = e.ErrorMessage });
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        return StatusCode((int)HttpStatusCode.Created, new ResponseBody() { Message = "Register completed." });
    }
}