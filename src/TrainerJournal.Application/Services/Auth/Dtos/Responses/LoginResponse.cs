using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Auth.Dtos.Responses;

public class LoginResponse(Guid id, string userName, string token)
{
    [Required]
    public Guid Id { get; init; } = id;
    
    [Required]
    [DefaultValue("login")]
    public string UserName { get; init; } = userName;
    
    [Required]
    [DefaultValue("jwt-token")]
    public string Token { get; init; } = token;
}