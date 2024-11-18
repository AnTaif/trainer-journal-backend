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
    
    public AttendanceMark(Student student, Practice practice, DateTime practiceTime, DateTime date) : base(Guid.NewGuid())
    {
        StudentId = student.UserId;
        PracticeId = practice.Id;
        PracticeTime = practiceTime;
        Date = date;

        AddDomainEvent(new BalanceChangedEvent(student, practice.Price, student.Balance,
            BalanceChangeReason.MarkAttendance, date));
    }

    public void Unmark()
    {
        AddDomainEvent(new BalanceChangedEvent(Student, -Practice.Price, Student.Balance,
            BalanceChangeReason.UnmarkAttendance, DateTime.UtcNow));
    }
    
    public void ChangePractice(Guid newPracticeId, DateTime newPracticeTime)
    {
        PracticeId = newPracticeId;
        PracticeTime = newPracticeTime;
    }
}