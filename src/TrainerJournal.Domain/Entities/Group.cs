using TrainerJournal.Domain.Entities.Abstract;

namespace TrainerJournal.Domain.Entities;

public class Group : Entity<Guid>
{
    public string Name { get; private set; }
    
    public Guid TrainerId { get; private set; }
    public virtual Trainer Trainer { get; private set; } = null!;
    
    public Guid HallId { get; private set; }
    public virtual Hall Hall { get; private set; } = null!;
    
    public Group(string name, Guid trainerId, Guid hallId) : base(Guid.NewGuid())
    {
        Name = name;
        TrainerId = trainerId;
        HallId = hallId;
    }
}