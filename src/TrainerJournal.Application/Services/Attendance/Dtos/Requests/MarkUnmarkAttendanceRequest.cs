namespace TrainerJournal.Application.Services.Attendance.Dtos.Requests;

public class MarkUnmarkAttendanceRequest
{
    public Guid PracticeId { get; set; }

    /// <summary>
    /// Used to handle SchedulePractice attendance
    /// </summary>
    public DateTime PracticeTime { get; set; }

    public bool IsMarked { get; set; } = true;
}