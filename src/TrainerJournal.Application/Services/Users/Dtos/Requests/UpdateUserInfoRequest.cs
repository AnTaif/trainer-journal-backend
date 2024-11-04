using System.ComponentModel.DataAnnotations;
using TrainerJournal.Application.DataAnnotations;

namespace TrainerJournal.Application.Services.Users.Dtos.Requests;

public class UpdateUserInfoRequest(string? fullName, string? email, string? phone, string? gender, string? telegramUsername)
{
    public string? FullName { get; init; } = fullName;
    
    [EmailAddress]
    public string? Email { get; init; } = email;
    
    [Phone]
    public string? Phone { get; init; } = phone;
    
    [GenderEnum]
    public string? Gender { get; init; } = gender;
    
    public string? TelegramUsername { get; init; } = telegramUsername;
}