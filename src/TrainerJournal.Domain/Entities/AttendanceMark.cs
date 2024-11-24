using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Enums.BalanceChangeReason;
using TrainerJournal.Domain.Events;

namespace TrainerJournal.Domain.Entities;

public class AttendanceMark : Entity<Guid>
{
    public Guid StudentId { get; private set; }
    public Student Student { get; private set; } = null!;
    
    public Guid PracticeId { get; private set; }
    public Practice Practice { get; private set; } = null!;
    
    public DateTime PracticeTime { get; private set; }

    public DateTime Date { get; private set; }

    public AttendanceMark() : base(Guid.NewGuid()) { } // Constructor for ef core dbContext
    
    public AttendanceMark(Student student, Practice practice, DateTime practiceTime, DateTime? date = null) : base(Guid.NewGuid())
    {
        StudentId = student.Id;
        PracticeId = practice.Id;
        PracticeTime = practiceTime;
        Date = date ?? DateTime.UtcNow;

        student.UpdateBalance(-practice.Price, BalanceChangeReason.MarkAttendance, Date);
    }

    public void Unmark()
    {
        Student.UpdateBalance(Practice.Price, BalanceChangeReason.UnmarkAttendance, DateTime.UtcNow);
    }
    
    public void ChangePractice(Guid newPracticeId, DateTime newPracticeTime)
    {
        PracticeId = newPracticeId;
        PracticeTime = newPracticeTime;
    }
}