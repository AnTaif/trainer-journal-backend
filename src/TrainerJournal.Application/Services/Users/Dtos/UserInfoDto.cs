namespace TrainerJournal.Application.Services.Users.Dtos;

public record UserInfoDto(
    string FullName, 
    string UserName,
    string? Email,
    string? Phone,
    string Gender, 
    string? TelegramUsername);