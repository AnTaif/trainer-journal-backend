namespace TrainerJournal.Application.Services.Users.Dtos.Requests;

public record CreateUserRequest(string FullName, string? Email, string? Phone, string Gender);