using TrainerJournal.Domain.Entities.Abstract;

namespace TrainerJournal.Domain.Entities;

public class Visit : Entity<Guid>
{
    public Guid StudentId { get; private set; }
    public Student Student { get; private set; } = null!;
    
    public Guid SessionId { get; private set; }
    public Session Session { get; private set; } = null!;
    
    public Visit(Guid studentId, Guid sessionId) : base(Guid.NewGuid())
    {
        StudentId = studentId;
        SessionId = sessionId;
    }
}