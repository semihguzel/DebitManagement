using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using DebitManagement.Base;
using DebitManagement.Core.Entities;
using Microsoft.IdentityModel.Tokens;

namespace DebitManagement.Service.Helpers;

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

    public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512(passwordSalt);
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(passwordHash);
    }

    public static string CreateJwtToken(Core.Entities.User user, string jwtKey)
    {
        List<Claim> claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Username)
        };

        foreach (var userRole in user.UserRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, userRole.RoleName));
        }

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}