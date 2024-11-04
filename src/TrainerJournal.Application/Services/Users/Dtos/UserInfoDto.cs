using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Users.Dtos;

public class UserInfoDto(
    string fullName, 
    string username,
    string? email,
    string? phone,
    string gender, 
    string? telegramUsername)
{
    [Required]
    [DefaultValue("Фамилия Имя Отчество")]
    public string FullName { get; init; } = fullName;
    
    [Required]
    [DefaultValue("login")]
    public string Username { get; init; } = username;
    
    [Required]
    [DefaultValue("М")]
    public string Gender { get; init; } = gender;
    
    public string? Email { get; init; } = email;
    public string? Phone { get; init; } = phone;
    public string? TelegramUsername { get; init; } = telegramUsername;
}