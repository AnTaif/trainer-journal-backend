using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Students.Dtos.Responses;

public class CreateStudentResponse
{
    [Required]
    [DefaultValue("login")]
    public string Username { get; init; } = null!;

    [Required]
    [DefaultValue("password")]
    public string Password { get; init; } = null!;

    [Required]
    [DefaultValue("Фамилия Имя Отчество")]
    public string FullName { get; init; } = null!;
}