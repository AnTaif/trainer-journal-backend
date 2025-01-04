using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Auth.Dtos.Requests;

public class LoginRequest
{
    [Required]
    [DefaultValue("D.S.Smirnov")]
    public string Username { get; init; } = null!;

    [Required]
    [DefaultValue("Password123")]
    public string Password { get; init; } = null!;
}