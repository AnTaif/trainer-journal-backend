using TrainerJournal.Domain.Enums.PracticeType;

namespace TrainerJournal.Domain.Entities;

// TODO: если тренер изменил цену, новая цена применяется в том числе и на единичные занятия с типом "Тренировка"
/// <summary>
/// Единичное занятие, которое можно изменять
/// </summary>
/// <remarks>
/// При создании нового повторяемого расписания единичные тренировки, подпадающие под расписание - удаляются
/// </remarks>
public class SinglePractice(
    Guid groupId,
    float price,
    DateTime start,
    DateTime end,
    PracticeType practiceType,
    Guid trainerId,
    Guid? overridenPracticeId = null,
    DateTime? originalStart = null)
    : Practice(groupId, price, start, end, practiceType, trainerId)
{
    public bool IsCanceled { get; private set; }

    public string? CancelComment { get; private set; }
    
    public Guid? OverridenPracticeId { get; init; } = overridenPracticeId;
    public SchedulePractice? OverridenPractice { get; init; }

    /// <summary>
    /// Время, на котором должна была быть проекция перезаписанного SchedulePractice
    /// </summary>
    public DateTime? OriginalStart { get; init; } = originalStart;

    public void Update(Guid? groupId, DateTime? start, DateTime? end, PracticeType? practiceType, float? price)
    {
        GroupId = groupId ?? GroupId;
        Start = start ?? Start;
        End = end ?? End;
        PracticeType = practiceType ?? PracticeType;
        if (price != null) ChangePrice(price.Value);
    }

    public void Cancel(string comment = "")
    {
        IsCanceled = true;
        CancelComment = comment;
    }
    
    public void Uncancel()
    {
        IsCanceled = false;
        CancelComment = null;
    }
    
    public void ChangeTime(DateTime? start, DateTime? end)
    {
        Start = start ?? Start;
        End = end ?? End;
    }
    
    public void ChangeTrainer(Guid trainerId)
    {
        TrainerId = trainerId;
    }
    
    public void ChangePracticeType(PracticeType practiceType)
    {
        PracticeType = practiceType;
    }
}