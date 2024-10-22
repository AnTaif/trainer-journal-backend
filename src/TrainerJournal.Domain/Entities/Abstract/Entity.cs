namespace TrainerJournal.Domain.Entities.Abstract;

public abstract class Entity<TId>(TId id)
{
    public TId Id { get; init; } = id;
}