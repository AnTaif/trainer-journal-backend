using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Users.Dtos;

public class UserInfoDto
{
    [Required]
    [DefaultValue("Фамилия Имя Отчество")]
    public string FullName { get; init; } = null!;

    [Required]
    [DefaultValue("login")]
    public string Username { get; init; } = null!;

    [Required]
    [DefaultValue("М")]
    public string Gender { get; init; } = null!;

    public string? TelegramUsername { get; init; }
}