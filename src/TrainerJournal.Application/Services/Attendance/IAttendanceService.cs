using ErrorOr;
using TrainerJournal.Application.Services.Attendance.Dtos;
using TrainerJournal.Application.Services.Attendance.Dtos.Requests;
using TrainerJournal.Application.Services.Attendance.Dtos.Responses;

namespace TrainerJournal.Application.Services.Attendance;

public interface IAttendanceService
{
    public Task<ErrorOr<List<GetStudentAttendanceResponse>>> GetGroupAttendanceAsync(Guid userId, Guid groupId, DateTime start, DateTime end);
    
    public Task<ErrorOr<GetStudentAttendanceResponse>> GetStudentAttendanceAsync(Guid userId, Guid studentId, DateTime start, DateTime end);
    
    public Task<ErrorOr<AttendanceMarkDto?>> MarkUnmarkAttendanceAsync(Guid userId, Guid studentId, AttendanceMarkRequest request);

    //public Task<ErrorOr<bool>> UnmarkAttendanceAsync(Guid userId, AttendanceMarkRequest request);
}