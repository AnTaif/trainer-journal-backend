using ErrorOr;
using TrainerJournal.Application.Services.Attendance.Dtos;
using TrainerJournal.Application.Services.Attendance.Dtos.Requests;
using TrainerJournal.Application.Services.Attendance.Dtos.Responses;
using TrainerJournal.Application.Services.Groups;
using TrainerJournal.Application.Services.Practices;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.Attendance;

public class AttendanceService(
    IPracticeRepository practiceRepository,
    IGroupRepository groupRepository,
    IAttendanceRepository attendanceRepository) : IAttendanceService
{
    public async Task<ErrorOr<List<GetStudentAttendanceResponse>>> GetGroupAttendanceAsync(
        Guid userId, Guid groupId, DateTime start, DateTime end)
    {
        var group = await groupRepository.GetByIdAsync(groupId);
        if (group == null) return Error.NotFound(description: "Group not found");
        if (group.TrainerId != userId) return Error.Forbidden(description: "You are not a trainer of this group");
        
        var attendance = await attendanceRepository.GetAttendanceByGroupIdAsync(groupId, start, end);

        return attendance.ToResponses();
    }

    public async Task<ErrorOr<GetStudentAttendanceResponse>> GetStudentAttendanceAsync(
        Guid userId, Guid studentId, DateTime start, DateTime end)
    {
        var attendance = await attendanceRepository.GetByStudentIdAsync(studentId, start, end);

        return attendance.ToResponse();
    }

    public async Task<ErrorOr<AttendanceMarkDto?>> MarkUnmarkAttendanceAsync(Guid userId, Guid studentId, AttendanceMarkRequest request)
    {
        var mark = await attendanceRepository.GetByInfoAsync(studentId, request.PracticeId, request.PracticeTime);
        if (mark != null)
        {
            await UnmarkAttendanceAsync(mark);
            return new ErrorOr<AttendanceMarkDto?>().Value;
        }
        
        var practice = await practiceRepository.GetByIdAsync(request.PracticeId);

        if (practice == null) return Error.NotFound(description: "Practice not found");
        if (practice.TrainerId != userId) return Error.Forbidden(description: "You are not a trainer of this group");
        
        var newMark = new AttendanceMark(studentId, request.PracticeId, request.PracticeTime);
        
        attendanceRepository.Add(newMark);
        await attendanceRepository.SaveChangesAsync();

        return newMark.ToDto();
    }

    private async Task UnmarkAttendanceAsync(AttendanceMark attendanceMark)
    {
        attendanceRepository.Remove(attendanceMark);
        await attendanceRepository.SaveChangesAsync();
    }

    // public async Task<ErrorOr<bool>> UnmarkAttendanceAsync(Guid userId, AttendanceMarkRequest request)
    // {
    //     var attendanceMark = 
    //         await attendanceRepository.GetByInfoAsync(request.StudentId, request.PracticeId, request.PracticeTime);
    //     var practice = await practiceRepository.GetByIdAsync(request.PracticeId);
    //     if (practice == null) return Error.NotFound(description: "Practice not found");
    //     
    //     if (attendanceMark == null) return true;
    //     if (practice.TrainerId != userId) return Error.Forbidden(description: "You are not a trainer of this group");
    //     
    //     attendanceRepository.Remove(attendanceMark);
    //     await attendanceRepository.SaveChangesAsync();
    //
    //     return true;
    // }
}