using TrainerJournal.Domain.Entities.Abstract;

namespace TrainerJournal.Domain.Entities;

public class Hall : Entity<Guid>
{
    public string Location { get; private set; }
    
    public string Description { get; private set; }
    
    public Hall(string location, string description) : base(Guid.NewGuid())
    {
        Location = location;
        Description = description;
    }
}