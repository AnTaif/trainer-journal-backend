using TrainerJournal.Domain.Enums.PracticeType;

namespace TrainerJournal.Domain.Entities;

// TODO: если тренер изменил цену, новая цена применяется в том числе и на единичные занятия с типом "Тренировка"
/// <summary>
/// Единичное занятие, которое можно изменять
/// </summary>
/// <remarks>
/// При создании нового повторяемого расписания единичные тренировки, подпадающие под расписание - удаляются
/// </remarks>
public class SinglePractice : Practice
{
    public bool IsCanceled { get; private set; }

    public string? CancelComment { get; private set; }
    
    public Guid? OverridenPracticeId { get; init; }
    public SchedulePractice? OverridenPractice { get; init; }
    
    public SinglePractice(
        Guid groupId, 
        float price, 
        DateTime start, 
        DateTime end, 
        PracticeType practiceType, 
        Guid trainerId, 
        Guid hallId,
        Guid? overridenPracticeId = null)
        : base(groupId, price, start, end, practiceType, trainerId, hallId)
    {
        OverridenPracticeId = overridenPracticeId;
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
    
    public void ChangeHall(Guid hallId)
    {
        HallId = hallId;
    }
    
    public void ChangeTrainer(Guid trainerId)
    {
        TrainerId = trainerId;
    }
    
    public void ChangePrice(float price)
    {
        Price = price;
    }
    
    public void ChangePracticeType(PracticeType practiceType)
    {
        PracticeType = practiceType;
    }
}