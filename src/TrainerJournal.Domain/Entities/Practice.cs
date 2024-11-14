using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Enums.PracticeType;

namespace TrainerJournal.Domain.Entities;

public abstract class Practice(
    Guid groupId,
    float price,
    DateTime start,
    DateTime end,
    PracticeType practiceType,
    Guid trainerId)
    : Entity<Guid>(Guid.NewGuid())
{
    public float Price { get; protected set; } = price;

    public DateTime Start { get; protected set; } = start;

    public DateTime End { get; protected set; } = end;

    public Guid GroupId { get; protected set; } = groupId;
    public Group Group { get; protected set; } = null!;
    
    public Guid TrainerId { get; protected set; } = trainerId;
    public Trainer Trainer { get; protected set; } = null!;
    
    public PracticeType PracticeType { get; protected set; } = practiceType;
    
    public void ChangePrice(float price)
    {
        if (Math.Abs(price - Price) > 0.0001) 
            Price = price;
    }
}