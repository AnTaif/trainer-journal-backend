using System.Net;

namespace TrainerJournal.Domain.Common.Result;

public class Error(HttpStatusCode statusCode, string message)
{
    public HttpStatusCode StatusCode { get; } = statusCode;
    public string Message { get; } = message;

    public static Error NotFound(string message = "")
    {
        return new Error(HttpStatusCode.NotFound, message);
    }

    public static Error BadRequest(string message = "")
    {
        return new Error(HttpStatusCode.BadRequest, message);
    }

    public static Error Forbidden(string message = "")
    {
        return new Error(HttpStatusCode.Forbidden, message);
    }
}