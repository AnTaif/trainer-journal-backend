using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Domain.Events;

public class GroupChangedEvent(Group group,float? newPrice, string? newHallAddress) : DomainEvent
{
    public Group Group { get; init; } = group;
    
    public float? NewPrice { get; init; } = newPrice;

    public string? NewHallAddress { get; init; } = newHallAddress;
}