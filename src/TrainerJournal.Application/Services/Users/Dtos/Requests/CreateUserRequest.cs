using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TrainerJournal.Application.DataAnnotations;

namespace TrainerJournal.Application.Services.Users.Dtos.Requests;

public class CreateUserRequest(string fullName, string gender)
{
    [Required]
    [DefaultValue("Фамилия Имя Отчество")]
    public string FullName { get; init; } = fullName;
     
    [Required]
    [GenderEnum]
    [DefaultValue("М")]
    public string Gender { get; init; } = gender;
}