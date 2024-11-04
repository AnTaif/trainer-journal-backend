using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Auth.Dtos.Requests;

public class LoginRequest(string username, string password)
{
    [Required]
    [DefaultValue("login")]
    public string Username { get; init; } = username;
    [Required]
    [DefaultValue("password")]
    public string Password { get; init; } = password;
}