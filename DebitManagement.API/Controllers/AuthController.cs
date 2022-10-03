using System.Net;
using DebitManagement.API.Dto;
using DebitManagement.Base;
using DebitManagement.Base.Auth;
using DebitManagement.Data.Entities;
using DebitManagement.Data.Interfaces;
using DebitManagement.Service.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DebitManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IConfiguration _configuration;
    private readonly AuthService _authService;

    public AuthController(IUserRepository userRepository, IUserRoleRepository userRoleRepository,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _userRoleRepository = userRoleRepository;
        _configuration = configuration;
        _authService = new AuthService(_userRepository, userRoleRepository, _configuration);
    }

    [HttpPost("Register")]
    public async Task<ActionResult> Register([FromBody] UserDto request)
    {
        try
        {
            AuthHelper.ValidateRequest(request.Username, request.Password);
            await _authService.CheckForRegister(request.Username);

            AuthHelper.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var userRole = await _authService.GetRoleByName("User");

            var user = new User()
            {
                Username = request.Username, PasswordHash = passwordHash, PasswordSalt = passwordSalt,
                UserRoleId = userRole.Id
            };

            await _userRepository.Create(user);
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

    [HttpPost("Login")]
    public async Task<ActionResult> Login([FromBody] UserDto request)
    {
        string token = string.Empty;
        try
        {
            var jwtKey = _configuration.GetSection("AppSettings:JwtKey");
            if (jwtKey == null || jwtKey.Value == null)
                throw new HttpException(HttpStatusCode.InternalServerError, "");

            AuthHelper.ValidateRequest(request.Username, request.Password);

            var user = await _authService.CheckForLogin(request.Username, request.Password);

            token = AuthHelper.CreateJwtToken(user, jwtKey.Value);
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
            new ResponseBody() { Message = "Logged in successfully.", Token = token });
    }
}