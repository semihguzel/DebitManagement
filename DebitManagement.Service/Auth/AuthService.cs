using System.Net;
using DebitManagement.Base;
using DebitManagement.Core.Entities;
using DebitManagement.Core.Interfaces;
using DebitManagement.Service.Helpers;
using Microsoft.Extensions.Configuration;

namespace DebitManagement.Service.Auth;

public class AuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IConfiguration _configuration;

    public AuthService(IUserRepository userRepository, IUserRoleRepository userRoleRepository,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _userRoleRepository = userRoleRepository;
        _configuration = configuration;
    }

    public async Task CheckForRegister(string username)
    {
        var user = await _userRepository.GetByUsername((username));

        if (user != null)
            throw new HttpException(HttpStatusCode.NotAcceptable,
                "User already exists, please login with your credentials.");
    }

    public async Task<User> CheckForLogin(string username, string password)
    {
        var user = await _userRepository.GetByUsername(username);

        if (user == null)
            throw new HttpException(HttpStatusCode.NotAcceptable,
                "User not found. Please check your credentials.");

        var isAuthenticated = AuthHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt);

        if (!isAuthenticated)
            throw new HttpException(HttpStatusCode.BadRequest, "Password is not correct.");

        return user;
    }

    public async Task<UserRole> GetRoleByName(string roleName)
    {
        var userRole = await _userRoleRepository.GetRoleByName("User");

        if (userRole == null)
        {
            throw new HttpException(HttpStatusCode.InternalServerError,
                "There has been an error . Please try again later");
        }

        return userRole;
    }
}