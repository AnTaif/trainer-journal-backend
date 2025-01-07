using Microsoft.EntityFrameworkCore;
using TrainerJournal.Application.Services.Attendance;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Infrastructure.Common;

namespace TrainerJournal.Infrastructure.Data.Repositories;

public class AttendanceRepository(AppDbContext context) : BaseRepository(context), IAttendanceRepository
{
    private DbSet<AttendanceMark> attendanceMarks => dbContext.AttendanceMarks;

    public async Task<AttendanceMark?> FindByInfoAsync(string studentUsername, Guid practiceId, DateTime practiceTime)
    {
        return await attendanceMarks
            .Include(a => a.Practice)
            .Include(a => a.Student)
                .ThenInclude(s => s.User)
            .FirstOrDefaultAsync(a =>
                a.Student.User.UserName == studentUsername 
                && a.PracticeId == practiceId && a.PracticeTime == practiceTime);
    }

    public async Task<List<AttendanceMark>> SelectByPracticeAsync(Guid practiceId, DateTime practiceStart)
    {
        return await attendanceMarks
            .Include(a => a.Student)
                .ThenInclude(s => s.User)
            .Where(a => a.PracticeId == practiceId && a.PracticeTime == practiceStart)
            .OrderBy(a => a.Student.User.FullName.LastName)
            .ToListAsync();
    }

    public async Task<List<string>> SelectMarkedStudentsByPracticeAsync(Guid practiceId, DateTime practiceStart)
    {
        return await attendanceMarks
            .Include(a => a.Practice)
            .Include(a => a.Student)
                .ThenInclude(s => s.User)
            .Where(a => a.PracticeId == practiceId && a.PracticeTime == practiceStart)
            .OrderBy(a => a.Student.User.FullName.LastName)
            .Select(a => a.Student.User.UserName!)
            .ToListAsync();
    }

    public async Task<List<AttendanceMark>> SelectByStudentIdAsync(Guid studentId, DateTime start, DateTime end)
    {
        return await attendanceMarks
            .Where(a => a.StudentId == studentId)
            .Where(a => start <= a.PracticeTime && a.PracticeTime <= end)
            .OrderByDescending(a => a.PracticeTime)
            .ToListAsync();
    }

    public async Task<List<AttendanceMark>> SelectByStudentUsernameAsync(string studentUsername, DateTime start, DateTime end)
    {
        return await attendanceMarks
            .Include(a => a.Student)
                .ThenInclude(s => s.User)
            .Where(a => a.Student.User.UserName == studentUsername)
            .Where(a => start <= a.PracticeTime && a.PracticeTime <= end)
            .OrderByDescending(a => a.PracticeTime)
            .ToListAsync();
    }

    public async Task<List<AttendanceMark>> SelectByGroupIdAsync(Guid groupId, DateTime start, DateTime end)
    {
        return await attendanceMarks
            //.Include(a => a.Practice)
            .Include(a => a.Student)
                .ThenInclude(s => s.User)
            .Where(a => a.Practice.GroupId != null && a.Practice.GroupId == groupId)
            .Where(a => start <= a.PracticeTime && a.PracticeTime <= end)
            .OrderByDescending(a => a.PracticeTime)
            .ThenBy(a => a.Student.User.FullName.LastName)
            .ToListAsync();
    }

    public void Add(AttendanceMark attendanceMark)
    {
        attendanceMarks.Add(attendanceMark);
    }

    public void AddRange(List<AttendanceMark> addedAttendanceMarks)
    {
        attendanceMarks.AddRange(addedAttendanceMarks);
    }

    public void Remove(AttendanceMark attendanceMark)
    {
        attendanceMarks.Remove(attendanceMark);
    }

    public void RemoveRange(List<AttendanceMark> attendanceMarksToRemove)
    {
        attendanceMarks.RemoveRange(attendanceMarksToRemove);
    }
}