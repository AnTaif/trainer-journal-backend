using System.ComponentModel.DataAnnotations;
using TrainerJournal.Application.DataAnnotations;

namespace TrainerJournal.Application.Services.Users.Dtos.Requests;

public record UpdateUserInfoRequest(
    string? FullName,
    [EmailAddress]
    string? Email, 
    [Phone]
    string? Phone,
    [GenderEnum]
    string? Gender, 
    string? TelegramUsername);