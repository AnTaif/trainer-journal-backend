using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TrainerJournal.Application.DataAnnotations;

namespace TrainerJournal.Application.Services.Trainers.Dtos.Requests;

public class RegisterTrainerRequest
{
    [MinimumWordsCount(2)]
    [Required]
    [DefaultValue("Фамилия Имя Отчество")]
    public string FullName { get; init; } = null!;

    [Required]
    [DefaultValue("М")]
    [GenderEnum]
    public string Gender { get; init; } = null!;
    
    public string? Phone { get; init; }
    
    public string? Email { get; init; }
}