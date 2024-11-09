using Microsoft.EntityFrameworkCore;
using TrainerJournal.Application.Services.Attendance;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Infrastructure.Data.Repositories;

public class AttendanceRepository(AppDbContext dbContext) : IAttendanceRepository
{
    private DbSet<AttendanceMark> attendanceMarks => dbContext.AttendanceMarks;

    public async Task<AttendanceMark?> GetByInfoAsync(Guid studentId, Guid practiceId, DateTime practiceTime)
    {
        return await attendanceMarks
            .FirstOrDefaultAsync(a =>
                a.StudentId == studentId && a.PracticeId == practiceId && a.PracticeTime == practiceTime);
    }

    public async Task<List<AttendanceMark>> GetByStudentIdAsync(Guid studentId, DateTime start, DateTime end)
    {
        return await attendanceMarks
            .Where(a => a.StudentId == studentId)
            .Where(a => start <= a.PracticeTime && a.PracticeTime <= end)
            .ToListAsync();
    }

    public async Task<List<AttendanceMark>> GetAttendanceByGroupIdAsync(Guid groupId, DateTime start, DateTime end)
    {
        return await attendanceMarks
            //.Include(a => a.Practice)
            .Where(a => a.Practice.GroupId == groupId)
            .Where(a => start <= a.PracticeTime && a.PracticeTime <= end)
            .ToListAsync();
    }

    public void Add(AttendanceMark attendanceMark)
    {
        attendanceMarks.Add(attendanceMark);
    }

    public void Remove(AttendanceMark attendanceMark)
    {
        attendanceMarks.Remove(attendanceMark);
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}