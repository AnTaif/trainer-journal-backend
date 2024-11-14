using TrainerJournal.Domain.Enums.PracticeType;

namespace TrainerJournal.Domain.Entities;

/// <summary>
/// Основа повторяемого  занятия (на основе этого энтити генерируются тренировки на будущие недели) 
/// </summary>
public class SchedulePractice(
    Guid scheduleId,
    Guid groupId,
    float price,
    DateTime start,
    DateTime end,
    string hallAddress,
    PracticeType practiceType,
    Guid trainerId)
    : Practice(groupId, price, start, end, hallAddress, practiceType, trainerId)
{
    public Guid ScheduleId { get; private set; } = scheduleId;
    public Schedule Schedule { get; private set; } = null!;
    
    public static DateTime CombineDateAndTime(DateTime datePart, DateTime timePart)
    {
        return datePart.Date + timePart.TimeOfDay;
    }
}