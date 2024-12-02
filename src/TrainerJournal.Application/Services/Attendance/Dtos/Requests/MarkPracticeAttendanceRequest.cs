namespace TrainerJournal.Application.Services.Attendance.Dtos.Requests;

public class MarkPracticeAttendanceRequest
{
    public DateTime PracticeStart { get; init; }
    
    public List<string> MarkedUsernames { get; init; } = null!;
}