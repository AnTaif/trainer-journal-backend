namespace TrainerJournal.Application.Users.Dtos.Requests;

public record CreateUserRequest(string FullName, string? Email, string? Phone, string Gender, string? TelegramUsername);