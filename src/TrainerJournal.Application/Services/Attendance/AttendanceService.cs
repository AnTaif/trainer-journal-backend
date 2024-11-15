using ErrorOr;
using TrainerJournal.Application.Services.Attendance.Dtos;
using TrainerJournal.Application.Services.Attendance.Dtos.Requests;
using TrainerJournal.Application.Services.Attendance.Dtos.Responses;
using TrainerJournal.Application.Services.Groups;
using TrainerJournal.Application.Services.Practices;
using TrainerJournal.Application.Services.Students;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.Attendance;

public class AttendanceService(
    IStudentRepository studentRepository,
    IPracticeRepository practiceRepository,
    IGroupRepository groupRepository,
    IAttendanceRepository attendanceRepository) : IAttendanceService
{
    public async Task<ErrorOr<List<GetStudentAttendanceResponse>>> GetGroupAttendanceAsync(
        Guid userId, Guid groupId, DateTime start, DateTime end)
    {
        var group = await groupRepository.GetByIdAsync(groupId);
        if (group == null) return Error.NotFound("Group not found");
        if (group.TrainerId != userId) return Error.Forbidden("You are not a trainer of this group");
        
        var attendance = await attendanceRepository.GetAttendanceByGroupIdAsync(groupId, start, end);

        return attendance.ToResponses();
    }

    public async Task<ErrorOr<List<AttendanceMarkDto>>> GetStudentAttendanceAsync(
        Guid userId, string studentUsername, DateTime start, DateTime end)
    {
        var attendance = await attendanceRepository.GetByStudentUsernameAsync(studentUsername, start, end);

        return attendance.Select(a => a.ToDto()).ToList();
    }

    public async Task<ErrorOr<AttendanceMarkDto?>> MarkUnmarkAttendanceAsync(Guid userId, string studentUsername, MarkUnmarkAttendanceRequest request)
    {
        var student = await studentRepository.GetByUsernameAsync(studentUsername);
        if (student == null) return Error.NotFound("Student not found");
        
        var mark = await attendanceRepository.GetByInfoAsync(studentUsername, request.PracticeId, request.PracticeTime);
        if (mark != null && !request.IsMarked)
        {
            await UnmarkAttendanceAsync(mark);
            return new ErrorOr<AttendanceMarkDto?>().Value;
        }
        
        if (mark != null && request.IsMarked) return mark.ToDto();
        
        var practice = await practiceRepository.GetByIdAsync(request.PracticeId);

        if (practice == null) return Error.NotFound("Practice not found");
        if (practice.TrainerId != userId) return Error.Forbidden("You are not a trainer of this group");
        
        var newMark = new AttendanceMark(student.UserId, request.PracticeId, request.PracticeTime);
        
        attendanceRepository.Add(newMark);
        await attendanceRepository.SaveChangesAsync();

        return newMark.ToDto();
    }

    private async Task UnmarkAttendanceAsync(AttendanceMark attendanceMark)
    {
        attendanceRepository.Remove(attendanceMark);
        await attendanceRepository.SaveChangesAsync();
    }
}