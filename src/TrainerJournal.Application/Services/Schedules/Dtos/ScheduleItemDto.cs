using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TrainerJournal.Application.DataAnnotations;

namespace TrainerJournal.Application.Services.Schedules.Dtos;

public class ScheduleItemDto(
    Guid id,
    DateTime start,
    DateTime end,
    string groupName,
    string practiceType,
    float price,
    bool isCanceled)
{
    public Guid Id { get; init; } = id;
    public DateTime Start { get; init; } = start;
    public DateTime End { get; init; } = end;
    
    [Required]
    [DefaultValue("Команда 1")]
    public string GroupName { get; init; } = groupName;

    [Required]
    [PracticeEnum]
    [DefaultValue("Тренировка")]
    public string PracticeType { get; init; } = practiceType;
    
    public float Price { get; init; } = price;
    public bool IsCanceled { get; } = isCanceled;
}