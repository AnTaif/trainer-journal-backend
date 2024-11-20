namespace TrainerJournal.Application.Services.Attendance.Dtos.Requests;

public class MarkAttendanceRequest
{
    public Guid PracticeId { get; set; }

    /// <summary>
    /// Used to handle SchedulePractice attendance
    /// </summary>
    public DateTime PracticeTime { get; set; }
}