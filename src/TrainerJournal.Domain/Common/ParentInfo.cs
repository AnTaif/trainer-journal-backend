using System.ComponentModel.DataAnnotations.Schema;

namespace TrainerJournal.Domain.Common;

[ComplexType]
public class ParentInfo(string? name, string? contact)
{
    public string Name { get; private set; } = name ?? "";
    public string Contact { get; private set; } = contact ?? "";

    public void Update(ParentInfo other)
    {
        if (!string.IsNullOrEmpty(other.Name))
            Name = other.Name;
        if (!string.IsNullOrEmpty(other.Contact))
            Contact = other.Contact;
    }
}