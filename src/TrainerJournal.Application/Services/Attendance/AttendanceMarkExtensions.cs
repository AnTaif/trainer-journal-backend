using TrainerJournal.Application.Services.Attendance.Dtos;
using TrainerJournal.Application.Services.Attendance.Dtos.Responses;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.Attendance;

public static class AttendanceMarkExtensions
{
    public static List<GetStudentAttendanceResponse> ToResponses(this IEnumerable<AttendanceMark> attendanceMarks)
    {
        return attendanceMarks
            .GroupBy(a => a.StudentId)
            .Select(g => new GetStudentAttendanceResponse(
                g.Key,
                g.Select(a => new AttendanceMarkDto(
                    a.PracticeId, 
                    a.PracticeTime
                )).ToList()
            )).ToList();
    }
    
    public static AttendanceMarkDto ToDto(this AttendanceMark attendanceMark)
    {
        return new AttendanceMarkDto(attendanceMark.PracticeId, attendanceMark.PracticeTime);
    }
}