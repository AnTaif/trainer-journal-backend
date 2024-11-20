namespace TrainerJournal.Application.Services.Attendance.Dtos.Responses;

public class GetStudentAttendanceResponse
{
    public string Username { get; init; }
    
    public string FullName { get; init; }
    
    public float StartBalance { get; init; }
    
    public float Expenses { get; init; }
    
    public float Payments { get; init; }

    public float EndBalance { get; init; }
    
    public List<AttendanceMarkDto> Attendance { get; init; } = null!;
}