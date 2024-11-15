using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Schedules.Dtos;

public class PracticeTrainerDto
{
    public Guid Id { get; init; }
    
    [Required]
    [DefaultValue("Фамилия Имя Отчество")]
    public string FullName { get; init; }
}