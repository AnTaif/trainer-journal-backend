using TrainerJournal.Application.DataAnnotations;

namespace TrainerJournal.Application.Services.Users.Dtos.Requests;

public class UpdateUserInfoRequest
{
    public string? FullName { get; init; }

    [GenderEnum]
    public string? Gender { get; init; }

    public string? TelegramUsername { get; init; }
}