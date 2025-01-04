using TrainerJournal.Domain.Common;

namespace TrainerJournal.Domain.Entities;

public class Schedule(Guid groupId, DateTime startDay, DateTime? until = null) : Entity<Guid>(Guid.NewGuid())
{
    public Guid GroupId { get; private set; } = groupId;
    public Group Group { get; private set; } = null!;
    
    public DateTime StartDay { get; private set; } = startDay;
    public DateTime? Until { get; private set; } = until;
    public List<SchedulePractice> Practices { get; private set; } = null!;

    public void SetUntil(DateTime? until)
    {
        Until = until;
    }
}