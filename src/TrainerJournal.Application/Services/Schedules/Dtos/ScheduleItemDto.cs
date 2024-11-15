using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TrainerJournal.Application.DataAnnotations;

namespace TrainerJournal.Application.Services.Schedules.Dtos;

public class ScheduleItemDto
{
    public Guid Id { get; init; }
    
    public DateTime Start { get; init; }
    
    public DateTime End { get; init; }
    
    [Required]
    [DefaultValue("Команда 1")]
    public string GroupName { get; init; }

    public string HallAddress { get; init; }

    [Required]
    [PracticeEnum]
    [DefaultValue("Тренировка")]
    public string PracticeType { get; init; }
    
    public float Price { get; init; }
    
    public bool IsCanceled { get; init; }
}