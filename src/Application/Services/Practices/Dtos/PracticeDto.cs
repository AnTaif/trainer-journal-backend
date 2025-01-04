using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TrainerJournal.Application.DataAnnotations;
using TrainerJournal.Application.Services.Schedules.Dtos;

namespace TrainerJournal.Application.Services.Practices.Dtos;

public class PracticeDto
{
    public Guid Id { get; init; }
    
    public DateTime Start { get; init; }
    
    public DateTime End { get; init; }
    
    public PracticeGroupDto? Group { get; init; }
    
    public PracticeTrainerDto Trainer { get; init; }

    [Required]
    [PracticeEnum]
    [DefaultValue("Тренировка")]
    public string PracticeType { get; init; }

    [Required]
    [DefaultValue("Улица д.101")]
    public string HallAddress { get; init; }

    public float Price { get; init; }
    
    public bool IsCanceled { get; init; }
    
    public string? CancelComment { get; init; }
}