namespace TrainerJournal.Application.Services.Attendance.Dtos;

public class AttendanceMarkDto(Guid id, Guid studentId, Guid practiceId, DateTime practiceTime)
{
    public Guid Id { get; init; } = id;

    public Guid StudentId { get; init; } = studentId;

    public Guid PracticeId { get; init; } = practiceId;

    public DateTime PracticeTime { get; init; } = practiceTime;
}