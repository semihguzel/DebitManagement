using System.Net;

namespace DebitManagement.Base;

public class HttpException : Exception
{
    public HttpStatusCode StatusCode { get; set; }
    public string ErrorMessage { get; set; }

    public HttpException(HttpStatusCode statusCode, string message)
    {
        StatusCode = statusCode;
        ErrorMessage = message;
    }
}