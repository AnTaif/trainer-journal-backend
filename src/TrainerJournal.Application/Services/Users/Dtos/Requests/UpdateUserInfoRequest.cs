namespace TrainerJournal.Application.Services.Users.Dtos.Requests;

public record UpdateUserInfoRequest(string? FullName, string? Email, string? Phone, string? Gender, string? TelegramUsername);