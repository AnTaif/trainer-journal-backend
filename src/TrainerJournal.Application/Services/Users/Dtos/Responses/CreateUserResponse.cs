using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Users.Dtos.Responses;

public class CreateUserResponse
{
    [Required]
    [DefaultValue("Фамилия Имя Отчество")]
    public string FullName { get; init; }
    
    [Required]
    [DefaultValue("login")]
    public string Username { get; init; }
    
    [Required]
    [DefaultValue("password")]
    public string Password { get; init; }
}