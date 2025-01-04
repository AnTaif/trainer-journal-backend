using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.Attendance;

public interface IAttendanceRepository
{
    public Task<AttendanceMark?> GetByInfoAsync(string studentUsername, Guid practiceId, DateTime practiceTime);

    public Task<List<AttendanceMark>> GetByPracticeAsync(Guid practiceId, DateTime practiceStart);

    public Task<List<string>> GetMarkedStudentsByPracticeAsync(Guid practiceId, DateTime practiceStart);
    
    public Task<List<AttendanceMark>> GetByStudentIdAsync(Guid studentId, DateTime start, DateTime end);
    
    public Task<List<AttendanceMark>> GetByStudentUsernameAsync(string studentUsername, DateTime start, DateTime end);

    public Task<List<AttendanceMark>> GetAttendanceByGroupIdAsync(Guid groupId, DateTime start, DateTime end);

    public void Add(AttendanceMark attendanceMark);

    public void AddRange(List<AttendanceMark> addedAttendanceMarks);

    public void Remove(AttendanceMark attendanceMark);

    public void RemoveRange(List<AttendanceMark> attendanceMarksToRemove);

    public Task SaveChangesAsync();
}