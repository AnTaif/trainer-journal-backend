namespace TrainerJournal.Application.Auth.Responses;

public record LoginResponse(Guid Id, string UserName, string Token);