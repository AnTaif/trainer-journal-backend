using TrainerJournal.Domain.Common;

namespace TrainerJournal.Domain.Entities;

public class Trainer : Entity<Guid>
{
    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;
    
    public Trainer(Guid userId) : base(Guid.NewGuid())
    {
        UserId = userId;
    }
}