namespace TrainerJournal.Application.Services.Attendance.Dtos;

public class AttendanceMarkDto(Guid practiceId, DateTime practiceTime)
{
    public Guid PracticeId { get; init; } = practiceId;

    public DateTime PracticeTime { get; init; } = practiceTime;
}