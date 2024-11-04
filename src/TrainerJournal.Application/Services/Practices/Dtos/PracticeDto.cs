using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TrainerJournal.Application.DataAnnotations;
using TrainerJournal.Application.Services.Schedule.Dtos;

namespace TrainerJournal.Application.Services.Practices.Dtos;

public class PracticeDto(
    Guid id,
    DateTime start,
    DateTime end,
    PracticeGroupDto group,
    PracticeTrainerDto trainer,
    string practiceType,
    float price)
{
    public Guid Id { get; init; } = id;
    public DateTime Start { get; init; } = start;
    public DateTime End { get; init; } = end;
    public PracticeGroupDto Group { get; init; } = group;
    public PracticeTrainerDto Trainer { get; init; } = trainer;

    [Required]
    [PracticeEnum]
    [DefaultValue("Тренировка")]
    public string PracticeType { get; init; } = practiceType;
    
    public float Price { get; init; } = price;
}