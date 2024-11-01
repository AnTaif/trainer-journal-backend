using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Enums.PracticeType;

namespace TrainerJournal.Domain.Entities;

public abstract class Practice : Entity<Guid>
{
    public float Price { get; protected set; }
    
    public DateTime Start { get; protected set; }
    
    public DateTime End { get; protected set; }
    
    public Guid GroupId { get; protected set; }
    public Group Group { get; protected set; } = null!;
    
    public Guid TrainerId { get; protected set; }
    public Trainer Trainer { get; protected set; } = null!;
    
    public Guid HallId { get; protected set; }
    public Hall Hall { get; protected set; } = null!;
    
    public PracticeType PracticeType { get; protected set; }
    
    //TODO: при изменении цены тренировок необходимо создавать новый SchedulePractice 
    protected Practice(
        Guid groupId, 
        float price, 
        DateTime start, 
        DateTime end,
        PracticeType practiceType, 
        Guid trainerId, 
        Guid hallId) : base(Guid.NewGuid())
    {
        GroupId = groupId;
        Price = price;
        Start = start;
        End = end;
        PracticeType = practiceType;
        TrainerId = trainerId;
        HallId = hallId;
    }
}