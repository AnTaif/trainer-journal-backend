using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.Attendance;

public interface IAttendanceRepository
{
    public Task<AttendanceMark?> GetByInfoAsync(string studentUsername, Guid practiceId, DateTime practiceTime);
    
    public Task<List<AttendanceMark>> GetByStudentIdAsync(Guid studentId, DateTime start, DateTime end);
    
    public Task<List<AttendanceMark>> GetByStudentUsernameAsync(string studentUsername, DateTime start, DateTime end);

    public Task<List<AttendanceMark>> GetAttendanceByGroupIdAsync(Guid groupId, DateTime start, DateTime end);

    public void Add(AttendanceMark attendanceMark);

    public void Remove(AttendanceMark attendanceMark);

    public Task SaveChangesAsync();
}