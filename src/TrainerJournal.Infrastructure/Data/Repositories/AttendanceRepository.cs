using Microsoft.EntityFrameworkCore;
using TrainerJournal.Application.Services.Attendance;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Infrastructure.Common;

namespace TrainerJournal.Infrastructure.Data.Repositories;

public class AttendanceRepository(AppDbContext context) : BaseRepository(context), IAttendanceRepository
{
    private DbSet<AttendanceMark> attendanceMarks => dbContext.AttendanceMarks;

    public async Task<AttendanceMark?> GetByInfoAsync(string studentUsername, Guid practiceId, DateTime practiceTime)
    {
        return await attendanceMarks
            .Include(a => a.Practice)
            .Include(a => a.Student)
                .ThenInclude(s => s.User)
            .FirstOrDefaultAsync(a =>
                a.Student.User.UserName == studentUsername 
                && a.PracticeId == practiceId && a.PracticeTime == practiceTime);
    }

    public async Task<List<AttendanceMark>> GetByPracticeAsync(Guid practiceId, DateTime practiceStart)
    {
        return await attendanceMarks
            .Include(a => a.Student)
                .ThenInclude(s => s.User)
            .Where(a => a.PracticeId == practiceId && a.PracticeTime == practiceStart)
            .ToListAsync();
    }

    public async Task<List<string>> GetMarkedStudentsByPracticeAsync(Guid practiceId, DateTime practiceStart)
    {
        return await attendanceMarks
            .Include(a => a.Practice)
            .Include(a => a.Student)
                .ThenInclude(s => s.User)
            .Where(a => a.PracticeId == practiceId && a.PracticeTime == practiceStart)
            .Select(a => a.Student.User.UserName!)
            .ToListAsync();
    }

    public async Task<List<AttendanceMark>> GetByStudentIdAsync(Guid studentId, DateTime start, DateTime end)
    {
        return await attendanceMarks
            .Where(a => a.StudentId == studentId)
            .Where(a => start <= a.PracticeTime && a.PracticeTime <= end)
            .ToListAsync();
    }

    public async Task<List<AttendanceMark>> GetByStudentUsernameAsync(string studentUsername, DateTime start, DateTime end)
    {
        return await attendanceMarks
            .Include(a => a.Student)
                .ThenInclude(s => s.User)
            .Where(a => a.Student.User.UserName == studentUsername)
            .Where(a => start <= a.PracticeTime && a.PracticeTime <= end)
            .ToListAsync();
    }

    public async Task<List<AttendanceMark>> GetAttendanceByGroupIdAsync(Guid groupId, DateTime start, DateTime end)
    {
        return await attendanceMarks
            //.Include(a => a.Practice)
            .Include(a => a.Student)
                .ThenInclude(s => s.User)
            .Where(a => a.Practice.GroupId != null && a.Practice.GroupId == groupId)
            .Where(a => start <= a.PracticeTime && a.PracticeTime <= end)
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