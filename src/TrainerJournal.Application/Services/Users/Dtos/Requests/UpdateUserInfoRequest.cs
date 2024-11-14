using TrainerJournal.Application.DataAnnotations;

namespace TrainerJournal.Application.Services.Users.Dtos.Requests;

public class UpdateUserInfoRequest(string? fullName, string? gender, string? telegramUsername)
{
    public string? FullName { get; init; } = fullName;
    
    [GenderEnum]
    public string? Gender { get; init; } = gender;
    
    public string? TelegramUsername { get; init; } = telegramUsername;
}