using TrainerJournal.Domain.Common;

namespace TrainerJournal.Domain.Entities;

public class ExtraContact(string? name, string? contact) : Entity<Guid>(Guid.NewGuid())
{
    public string Name { get; private set; } = name ?? "";
    public string Contact { get; private set; } = contact ?? "";

    public void Update(string? name, string? contact)
    {
        Name = name ?? Name;
        Contact = contact ?? Contact;
    }
}