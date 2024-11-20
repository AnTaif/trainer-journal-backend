using ErrorOr;
using TrainerJournal.Application.Services.Attendance.Dtos;
using TrainerJournal.Application.Services.Attendance.Dtos.Requests;
using TrainerJournal.Application.Services.Attendance.Dtos.Responses;

namespace TrainerJournal.Application.Services.Attendance;

public interface IAttendanceService
{
    public Task<ErrorOr<List<GetStudentAttendanceResponse>>> GetGroupAttendanceAsync(Guid userId, Guid groupId, DateTime start, DateTime end);
    
    public Task<ErrorOr<List<AttendanceMarkDto>>> GetStudentAttendanceAsync(Guid userId, string studentName, DateTime start, DateTime end);
    
    public Task<ErrorOr<AttendanceMarkDto?>> MarkAttendanceAsync(Guid userId, string studentUsername, MarkAttendanceRequest request);
    
    public Task<ErrorOr<bool>> UnmarkAttendanceAsync(Guid userId, string studentUsername, MarkAttendanceRequest request);
}