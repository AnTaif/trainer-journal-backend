using TrainerJournal.Domain.Common;

namespace TrainerJournal.Domain.Entities;

public class Contact : Entity<Guid>
{
    public string Name { get; init; } = "";

    public string Relation { get; init; } = "";

    public string Phone { get; init; } = "";

    public Contact() : base(Guid.NewGuid()) { }
    
    public Contact(string? name, string? relation, string? contact) : base(Guid.NewGuid())
    {
        Name = name ?? "";
        Relation = relation ?? "";
        Phone = contact ?? "";
    }
}