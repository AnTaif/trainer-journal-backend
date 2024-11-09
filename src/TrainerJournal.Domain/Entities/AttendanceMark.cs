using TrainerJournal.Domain.Common;

namespace TrainerJournal.Domain.Entities;

public class AttendanceMark(Guid studentId, Guid practiceId, DateTime practiceTime) : Entity<Guid>(Guid.NewGuid())
{
    public Guid StudentId { get; private set; } = studentId;
    public Student Student { get; private set; } = null!;
    
    public Guid PracticeId { get; private set; } = practiceId;
    public Practice Practice { get; private set; } = null!;
    
    public DateTime PracticeTime { get; private set; } = practiceTime;
    
    public void ChangePractice(Guid newPracticeId, DateTime newPracticeTime)
    {
        PracticeId = newPracticeId;
        PracticeTime = newPracticeTime;
    }
}