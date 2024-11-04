using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Users.Dtos.Responses;

public class CreateUserResponse(
    Guid id,
    string password,
    string fullName, 
    string username,
    string? email,
    string? phone,
    string gender, 
    string? telegramUsername)
{
    public Guid Id { get; init; } = id;
    
    [Required]
    [DefaultValue("password")]
    public string Password { get; init; } = password;
    
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