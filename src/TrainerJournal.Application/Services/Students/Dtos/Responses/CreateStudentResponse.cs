using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Students.Dtos.Responses;

public class CreateStudentResponse(Guid id, string username, string password, string fullName)
{
    public Guid Id { get; init; } = id;
    
    [Required]
    [DefaultValue("login")]
    public string Username { get; init; } = username;
    
    [Required]
    [DefaultValue("password")]
    public string Password { get; init; } = password;
    
    [Required]
    [DefaultValue("Фамилия Имя Отчество")]
    public string FullName { get; init; } = fullName;
}