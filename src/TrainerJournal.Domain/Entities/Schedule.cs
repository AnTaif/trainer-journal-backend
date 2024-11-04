using TrainerJournal.Domain.Common;

namespace TrainerJournal.Domain.Entities;

public class Schedule(DateTime start, DateTime? until = null) : Entity<Guid>(Guid.NewGuid())
{
    public DateTime Start { get; private set; } = start;

    public DateTime? Until { get; private set; } = until;

    public List<SchedulePractice> Practices { get; private set; } = null!;

    public void SetUntil(DateTime? until)
    {
        Until = until;
    }
}