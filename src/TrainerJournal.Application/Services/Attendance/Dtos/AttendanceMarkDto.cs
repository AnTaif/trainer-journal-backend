namespace TrainerJournal.Application.Services.Attendance.Dtos;

public class AttendanceMarkDto
{
    public Guid PracticeId { get; init; }

    public DateTime PracticeTime { get; init; }
}