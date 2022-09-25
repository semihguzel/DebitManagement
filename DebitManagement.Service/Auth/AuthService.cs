using System.Net;
using DebitManagement.Base;
using DebitManagement.Data.Interfaces;

namespace DebitManagement.Service.Auth;

public class AuthService
{
    private readonly IUserRepository _repository;

    public AuthService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task CheckForRegister(string username)
    {
        var user = await _repository.GetByUsername((username));

        if (user != null)
            throw new HttpException(HttpStatusCode.NotAcceptable,"User already exists, please login with your credentials.");
    }
}