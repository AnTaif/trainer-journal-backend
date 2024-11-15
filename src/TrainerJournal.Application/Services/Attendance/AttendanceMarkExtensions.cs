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
            .Select(g => new GetStudentAttendanceResponse
        {
            StudentId = g.Key,
            Attendance = g.Select(a => a.ToDto()).ToList()
        }).ToList();
    }
    
    public static AttendanceMarkDto ToDto(this AttendanceMark attendanceMark)
    {
        return new AttendanceMarkDto
        {
            PracticeId = attendanceMark.PracticeId,
            PracticeTime = attendanceMark.PracticeTime
        };
    }
}