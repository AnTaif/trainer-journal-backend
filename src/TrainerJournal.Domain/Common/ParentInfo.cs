using System.ComponentModel.DataAnnotations.Schema;

namespace TrainerJournal.Domain.Common;

[ComplexType]
public class ParentInfo(string name, string contact)
{
    public string Name { get; set; } = name;
    public string Contact { get; set; } = contact;

    public void Update(string? name, string? contact)
    {
        Name = string.IsNullOrEmpty(name) ? Name : name;
        Contact = string.IsNullOrEmpty(contact) ? Contact : contact;
    }
}