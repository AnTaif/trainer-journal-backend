using Microsoft.Extensions.Logging;
using TrainerJournal.Application.Services.Attendance.Dtos;
using TrainerJournal.Application.Services.Attendance.Dtos.Requests;
using TrainerJournal.Application.Services.Attendance.Dtos.Responses;
using TrainerJournal.Application.Services.BalanceChanges;
using TrainerJournal.Application.Services.Groups;
using TrainerJournal.Application.Services.Practices;
using TrainerJournal.Application.Services.Students;
using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Enums.BalanceChangeReason;

namespace TrainerJournal.Application.Services.Attendance;

public class AttendanceService(
    ILogger<AttendanceService> logger,
    IPracticeManager practiceManager,
    IBalanceChangeManager balanceChangeManager,
    IStudentRepository studentRepository,
    IGroupRepository groupRepository,
    IAttendanceRepository attendanceRepository) : IAttendanceService
{
    public async Task<Result<List<GetStudentAttendanceResponse>>> GetGroupAttendanceAsync(
        Guid userId, Guid groupId, DateTime start, DateTime end)
    {
        var group = await groupRepository.GetByIdAsync(groupId);
        if (group == null) return Error.NotFound("Group not found");
        if (group.TrainerId != userId) return Error.Forbidden("You are not a trainer of this group");

        var attendance = await attendanceRepository.GetAttendanceByGroupIdAsync(groupId, start, end);

        var finances =
            new Dictionary<Guid, (float StartBalance, float Expenses, float Payments, float EndBalance)>();

        foreach (var student in group.Students)
        {
            var balanceReport = await balanceChangeManager.GetStudentBalanceReport(
                student.Id, start, end);

            finances.Add(student.Id,
                (balanceReport.StartBalance, balanceReport.Expenses, balanceReport.Payments,
                    balanceReport.EndBalance));
        }

        return attendance.ToResponses(group.Students, finances);
    }

    public async Task<Result<List<AttendanceMarkDto>>> GetStudentAttendanceAsync(
        Guid userId, string studentUsername, DateTime start, DateTime end)
    {
        var attendance = await attendanceRepository.GetByStudentUsernameAsync(studentUsername, start, end);

        return attendance.Select(a => a.ToDto()).ToList();
    }

    public async Task<Result<AttendanceMarkDto?>> MarkAttendanceAsync(Guid userId, string studentUsername,
        MarkAttendanceRequest request)
    {
        var student = await studentRepository.GetByUsernameAsync(studentUsername);
        if (student == null) return Error.NotFound("Student not found");

        var mark = await attendanceRepository.GetByInfoAsync(studentUsername, request.PracticeId, request.PracticeStart);
        if (mark != null) return mark.ToDto();

        var practiceResult = await practiceManager.GetBasePracticeAsync(request.PracticeId, request.PracticeStart);
        if (practiceResult.IsError()) return practiceResult.Error;
        var practice = practiceResult.Value;
        
        if (practice.TrainerId != userId) return Error.Forbidden("You are not a trainer of this group");

        var newMark = new AttendanceMark(student, practice, request.PracticeStart, DateTime.UtcNow);

        await balanceChangeManager.ChangeBalanceAsync(student, -practice.Price, BalanceChangeReason.MarkAttendance);
        
        attendanceRepository.Add(newMark);
        await attendanceRepository.SaveChangesAsync();

        return newMark.ToDto();
    }

    public async Task<Result> UnmarkAttendanceAsync(Guid userId, string studentUsername,
        MarkAttendanceRequest request)
    {
        var student = await studentRepository.GetByUsernameWithIncludesAsync(studentUsername);
        if (student == null) return Error.NotFound("Student not found");

        var mark = await attendanceRepository.GetByInfoAsync(studentUsername, request.PracticeId, request.PracticeStart);
        if (mark == null) return Result.Success();

        await balanceChangeManager.ChangeBalanceAsync(student, mark.Practice.Price,
            BalanceChangeReason.UnmarkAttendance);
        
        attendanceRepository.Remove(mark);
        
        await attendanceRepository.SaveChangesAsync();
        return Result.Success();
    }

    public async Task<Result<List<GetPracticeAttendanceResponse>>> GetPracticeAttendanceAsync(Guid userId, Guid practiceId, DateTime practiceStart)
    {
        var practiceResult = await practiceManager.GetBasePracticeWithIncludesAsync(practiceId, practiceStart);
        if (practiceResult.IsError()) return practiceResult.Error;
        var practice = practiceResult.Value;

        var usernames =
            new HashSet<string>(await attendanceRepository.GetMarkedStudentsByPracticeAsync(practiceId, practiceStart));

        return practice.Group.Students.Select(s => 
            new GetPracticeAttendanceResponse
            {
                Username = s.User.UserName!,
                FullName = s.User.FullName.ToString(),
                IsMarked = usernames.Contains(s.User.UserName!)
            }).ToList();
    }

    public async Task<Result<List<GetPracticeAttendanceResponse>>> MarkPracticeAttendanceAsync(Guid userId, Guid practiceId,
        MarkPracticeAttendanceRequest request)
    {
        var practiceResult = await practiceManager.GetBasePracticeWithIncludesAsync(practiceId, request.PracticeStart);
        if (practiceResult.IsError()) return practiceResult.Error;
        var practice = practiceResult.Value;

        var practiceMarks = await attendanceRepository.GetByPracticeAsync(practiceId, request.PracticeStart);
        var practiceMarkByStudentId = practiceMarks.ToDictionary(a => a.StudentId);

        var previousMarkedUsernames = 
            new HashSet<string>(practiceMarks.Select(m => m.Student.User.UserName!));

        var currentMarkedUsernames = new HashSet<string>(request.MarkedUsernames);

        var notChangedUsernames = previousMarkedUsernames.Intersect(currentMarkedUsernames).ToHashSet();
        var markedUsernames = currentMarkedUsernames.Except(previousMarkedUsernames).ToHashSet();

        var newAttendanceMarks = new List<AttendanceMark>();
        var attendanceMarksToRemove = new List<AttendanceMark>();

        foreach (var student in practice.Group.Students)
        {
            var user = student.User;
            if (notChangedUsernames.Contains(user.UserName!)) continue;

            if (markedUsernames.Contains(user.UserName!))
            {
                var mark = new AttendanceMark(student, practice, request.PracticeStart);
                
                await balanceChangeManager.ChangeBalanceAsync(student, -practice.Price,
                    BalanceChangeReason.MarkAttendance);
                
                newAttendanceMarks.Add(mark);
            }
            else
            {
                var mark = practiceMarkByStudentId.GetValueOrDefault(student.Id);
                if (mark == null)
                {
                    logger.LogError("MarkPracticeAttendanceAsync: Can't find AttendanceMark to delete");
                    continue;
                }
                
                await balanceChangeManager.ChangeBalanceAsync(student, mark.Practice.Price,
                    BalanceChangeReason.UnmarkAttendance);
                
                attendanceMarksToRemove.Add(mark);
            }
        }

        attendanceRepository.RemoveRange(attendanceMarksToRemove);
        attendanceRepository.AddRange(newAttendanceMarks);
        await attendanceRepository.SaveChangesAsync();
        
        return practice.Group.Students.Select(s => 
            new GetPracticeAttendanceResponse
            {
                Username = s.User.UserName!,
                FullName = s.User.FullName.ToString(),
                IsMarked = currentMarkedUsernames.Contains(s.User.UserName!)
            }).ToList();
    }
}