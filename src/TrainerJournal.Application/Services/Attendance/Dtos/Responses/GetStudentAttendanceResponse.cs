namespace TrainerJournal.Application.Services.Attendance.Dtos.Responses;

public class GetStudentAttendanceResponse(Guid studentId, List<AttendanceMarkDto> attendance)
{
    public Guid StudentId { get; init; } = studentId;

    public List<AttendanceMarkDto> Attendance { get; init; } = attendance;
}