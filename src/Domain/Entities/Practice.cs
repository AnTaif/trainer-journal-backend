using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Enums.PracticeType;

namespace TrainerJournal.Domain.Entities;

public abstract class Practice(
    Guid? groupId,
    float price,
    DateTime start,
    DateTime end,
    string hallAddress,
    PracticeType practiceType,
    Guid trainerId)
    : Entity<Guid>(Guid.NewGuid())
{
    public float Price { get; protected set; } = price;

    public DateTime Start { get; protected set; } = start;

    public DateTime End { get; protected set; } = end;

    public Guid? GroupId { get; protected set; } = groupId;
    public Group Group { get; protected set; } = null!;
    
    public Guid TrainerId { get; protected set; } = trainerId;
    public Trainer Trainer { get; protected set; } = null!;

    public string HallAddress { get; protected set; } = hallAddress;
    
    public PracticeType PracticeType { get; protected set; } = practiceType;

    public void ChangeHallAddress(string hallAddress)
    {
        HallAddress = hallAddress;
    }
    
    public void ChangePrice(float price)
    {
        if (Math.Abs(price - Price) > 0.0001) 
            Price = price;
    }

    public override bool Equals(object? obj)
    {
        return obj is Practice practice && Equals(practice);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }

    private bool Equals(Practice other)
    {
        return Math.Abs(Price - other.Price) < 0.0001
               && GroupId == other.GroupId
               && TrainerId == other.TrainerId
               && HallAddress == other.HallAddress
               && PracticeType == other.PracticeType;
    }
}