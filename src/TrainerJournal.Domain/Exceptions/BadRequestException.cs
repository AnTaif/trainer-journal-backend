using System.Net;

namespace TrainerJournal.Domain.Exceptions;

public class BadRequestException(string? text) : ResponseStatusException(text, HttpStatusCode.BadRequest);