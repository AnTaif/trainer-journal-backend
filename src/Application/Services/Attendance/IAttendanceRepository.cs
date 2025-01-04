using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.Attendance;

public interface IAttendanceRepository : IUnitOfWork
{
    Task<AttendanceMark?> FindByInfoAsync(string studentUsername, Guid practiceId, DateTime practiceTime);

    Task<List<AttendanceMark>> SelectByPracticeAsync(Guid practiceId, DateTime practiceStart);

    Task<List<string>> SelectMarkedStudentsByPracticeAsync(Guid practiceId, DateTime practiceStart);
    
    Task<List<AttendanceMark>> SelectByStudentIdAsync(Guid studentId, DateTime start, DateTime end);
    
    Task<List<AttendanceMark>> SelectByStudentUsernameAsync(string studentUsername, DateTime start, DateTime end);

    Task<List<AttendanceMark>> SelectByGroupIdAsync(Guid groupId, DateTime start, DateTime end);

    void Add(AttendanceMark attendanceMark);

    void AddRange(List<AttendanceMark> addedAttendanceMarks);

    void Remove(AttendanceMark attendanceMark);

    void RemoveRange(List<AttendanceMark> attendanceMarksToRemove);
}