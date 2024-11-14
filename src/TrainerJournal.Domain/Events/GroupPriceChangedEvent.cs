using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Domain.Events;

public class GroupPriceChangedEvent(Group group, float oldPrice, float newPrice) : DomainEvent
{
    public Group Group { get; init; } = group;

    public float OldPrice { get; init; } = oldPrice;
    
    public float NewPrice { get; init; } = newPrice;
}