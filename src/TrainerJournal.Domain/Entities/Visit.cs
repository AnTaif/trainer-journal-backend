using TrainerJournal.Domain.Common;

namespace TrainerJournal.Domain.Entities;

public class Visit : Entity<Guid>
{
    public Guid StudentId { get; private set; }
    public Student Student { get; private set; } = null!;
    
    public Guid PracticeId { get; private set; }
    public Practice Practice { get; private set; } = null!;
    
    public Visit(Guid studentId, Guid practiceId) : base(Guid.NewGuid())
    {
        StudentId = studentId;
        PracticeId = practiceId;
    }
}