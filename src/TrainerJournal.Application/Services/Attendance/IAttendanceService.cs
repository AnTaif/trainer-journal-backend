using TrainerJournal.Application.Services.Attendance.Dtos;
using TrainerJournal.Application.Services.Attendance.Dtos.Requests;
using TrainerJournal.Application.Services.Attendance.Dtos.Responses;
using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Common.Result;

namespace TrainerJournal.Application.Services.Attendance;

public interface IAttendanceService
{
    public Task<Result<List<GetStudentAttendanceResponse>>> GetGroupAttendanceAsync(Guid userId, Guid groupId,
        DateTime start, DateTime end);

    public Task<Result<List<AttendanceMarkDto>>> GetStudentAttendanceAsync(Guid userId, string studentName,
        DateTime start, DateTime end);

    public Task<Result<AttendanceMarkDto?>> MarkAttendanceAsync(Guid userId, string studentUsername,
        MarkAttendanceRequest request);

    public Task<Result> UnmarkAttendanceAsync(Guid userId, string studentUsername, MarkAttendanceRequest request);

    public Task<Result<List<GetPracticeAttendanceResponse>>> GetPracticeAttendanceAsync(Guid userId, Guid practiceId,
        DateTime practiceStart);

    public Task<Result<List<GetPracticeAttendanceResponse>>> MarkPracticeAttendanceAsync(Guid userId, Guid practiceId, MarkPracticeAttendanceRequest request);
}