using System.Net;

namespace TrainerJournal.Domain.Exceptions;

public abstract class ResponseStatusException(string? text, HttpStatusCode statusCode) : Exception(text)
{
    public HttpStatusCode StatusCode { get; init; } = statusCode;
}