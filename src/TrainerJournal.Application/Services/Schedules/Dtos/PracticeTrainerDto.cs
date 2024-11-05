using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Schedules.Dtos;

public class PracticeTrainerDto(Guid id, string fullName)
{
    public Guid Id { get; } = id;
    
    [Required]
    [DefaultValue("Фамилия Имя Отчество")]
    public string FullName { get; } = fullName;
}