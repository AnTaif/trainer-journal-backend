namespace TrainerJournal.Domain.Exceptions;

public class BadRequestException(string? text) : Exception(text);