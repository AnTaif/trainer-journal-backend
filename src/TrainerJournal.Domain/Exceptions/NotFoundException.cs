using System.Net;

namespace TrainerJournal.Domain.Exceptions;

public class NotFoundException(string? text) : ResponseStatusException(text, HttpStatusCode.NotFound);