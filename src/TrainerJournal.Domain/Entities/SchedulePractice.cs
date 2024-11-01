using TrainerJournal.Domain.Enums.PracticeType;

namespace TrainerJournal.Domain.Entities;

/// <summary>
/// Основа повторяемого  занятия (на основе этого энтити генерируются тренировки на будущие недели) 
/// </summary>
public class SchedulePractice : Practice
{
    public DateTime? Until { get; private set; }
    
    public SchedulePractice(
        Guid groupId, 
        float price, 
        DateTime start, 
        DateTime end, 
        PracticeType practiceType, 
        Guid trainerId, 
        Guid hallId, 
        DateTime? until = null) 
        : base(groupId, price, start, end, practiceType, trainerId, hallId)
    {
        Until = until;
    }
    
    public void SetUntil(DateTime? until)
    {
        Until = until;
    }
}