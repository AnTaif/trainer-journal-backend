namespace TrainerJournal.Application.Services.Auth.Dtos.Responses;

public record LoginResponse(Guid Id, string UserName, string Token);