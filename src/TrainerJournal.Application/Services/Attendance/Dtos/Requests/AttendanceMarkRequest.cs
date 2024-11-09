namespace TrainerJournal.Application.Services.Attendance.Dtos.Requests;

/// <summary>
/// Dto model for Mark/Unmark attendance
/// </summary>
public class AttendanceMarkRequest(Guid practiceId, DateTime practiceTime)
{
    public Guid PracticeId { get; init; } = practiceId;

    /// <summary>
    /// Used to handle SchedulePractice attendance
    /// </summary>
    public DateTime PracticeTime { get; init; } = practiceTime;
}