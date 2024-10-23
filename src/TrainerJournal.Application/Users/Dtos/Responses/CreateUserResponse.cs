namespace TrainerJournal.Application.Users.Dtos.Responses;

public record CreateUserResponse(
    Guid Id,
    string Password,
    string FullName, 
    string UserName,
    string? Email,
    string? Phone,
    string Gender, 
    string? TelegramUsername);