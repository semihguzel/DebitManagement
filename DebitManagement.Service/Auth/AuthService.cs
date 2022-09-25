using System.Net;
using DebitManagement.Base;
using DebitManagement.Base.Auth;
using DebitManagement.Data.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DebitManagement.Service.Auth;

public class AuthService
{
    private readonly IUserRepository _repository;
    private readonly IConfiguration _configuration;

    public AuthService(IUserRepository repository, IConfiguration configuration)
    {
        _repository = repository;
        _configuration = configuration;
    }

    public async Task CheckForRegister(string username)
    {
        var user = await _repository.GetByUsername((username));

        if (user != null)
            throw new HttpException(HttpStatusCode.NotAcceptable,
                "User already exists, please login with your credentials.");
    }

    public async Task CheckForLogin(string username, string password)
    {
        var user = await _repository.GetByUsername(username);

        if (user == null)
            throw new HttpException(HttpStatusCode.NotAcceptable,
                "User not found. Please check your credentials.");

        var isAuthenticated = AuthHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt);

        if (!isAuthenticated)
            throw new HttpException(HttpStatusCode.BadRequest, "Password is not correct.");
    }
}