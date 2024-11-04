using TrainerJournal.Domain.Enums.PracticeType;

namespace TrainerJournal.Domain.Entities;

/// <summary>
/// Основа повторяемого  занятия (на основе этого энтити генерируются тренировки на будущие недели) 
/// </summary>
public class SchedulePractice(
    Guid groupId,
    float price,
    DateTime start,
    DateTime end,
    PracticeType practiceType,
    Guid trainerId)
    : Practice(groupId, price, start, end, practiceType, trainerId)
{
    public Guid ScheduleId { get; private set; }
    public Schedule Schedule { get; private set; } = null!;
}