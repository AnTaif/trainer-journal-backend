using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Auth.Dtos.Requests;

public class LoginRequest(string username, string password)
{
    [Required]
    [DefaultValue("D.S.Smirnov")]
    public string Username { get; init; } = username;
    
    [Required]
    [DefaultValue("Password123")]
    public string Password { get; init; } = password;
}