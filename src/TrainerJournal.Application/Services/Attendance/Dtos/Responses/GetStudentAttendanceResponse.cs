namespace TrainerJournal.Application.Services.Attendance.Dtos.Responses;

public class GetStudentAttendanceResponse
{
    public Guid StudentId { get; init; }

    public List<AttendanceMarkDto> Attendance { get; init; }
}