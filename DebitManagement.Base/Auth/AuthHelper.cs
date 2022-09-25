using System.Net;
using System.Security.Cryptography;

namespace DebitManagement.Base.Auth;

public static class AuthHelper
{
    public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    }

    public static void ValidateRequest(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            throw new HttpException(HttpStatusCode.NotAcceptable, "Username or password cannot be empty.");
        if (username.Length < 5)
            throw new HttpException(HttpStatusCode.NotAcceptable, "Username has to be longer than 5 characters");
        if (password.Length < 5)
            throw new HttpException(HttpStatusCode.NotAcceptable, "Password has to be longer than 5 characters");
    }
}