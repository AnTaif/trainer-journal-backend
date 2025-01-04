using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Users.Dtos.Responses;

public class RegisterUserResponse
{
    [Required]
    [DefaultValue("login")]
    public string Username { get; init; } = null!;

    [Required]
    [DefaultValue("password")]
    public string Password { get; init; } = null!;

    [Required]
    [DefaultValue("Фамилия Имя Отчество")]
    public string FullName { get; init; } = null!;
}