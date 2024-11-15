using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Auth.Dtos.Responses;

public class LoginResponse
{
    [Required]
    [DefaultValue("login")]
    public string UserName { get; init; }
    
    [Required]
    [DefaultValue("jwt-token")]
    public string Token { get; init; }
}