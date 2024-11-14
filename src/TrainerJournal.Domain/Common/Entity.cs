namespace TrainerJournal.Domain.Common;

public abstract class Entity<TId>(TId id)
{
    private readonly List<DomainEvent> domainEvents = [];
    public IReadOnlyCollection<DomainEvent> DomainEvents => domainEvents.AsReadOnly();
    
    public TId Id { get; init; } = id;
    
    protected void AddDomainEvent(DomainEvent domainEvent)
    {
        domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        domainEvents.Clear();
    }
}