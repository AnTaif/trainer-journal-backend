using TrainerJournal.Application.Services.Attendance.Dtos;
using TrainerJournal.Application.Services.Attendance.Dtos.Responses;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.Attendance;

public static class AttendanceMarkExtensions
{
    public static List<GetStudentAttendanceResponse> ToResponses(
        this IEnumerable<AttendanceMark> attendanceMarks,
        List<Student> students,
        IReadOnlyDictionary<Guid, (float StartBalance, float Expenses, float Payments, float EndBalance)> finances)
    {
        var studentAttendance = new Dictionary<Guid, List<AttendanceMark>>();

        foreach (var attendanceMark in attendanceMarks)
        {
            studentAttendance.TryAdd(attendanceMark.StudentId, []);
            studentAttendance[attendanceMark.StudentId].Add(attendanceMark);
        }

        return students
            .Select(s =>
            {
                var finance = finances[s.Id];
                studentAttendance.TryGetValue(s.Id, out var attendance);
                return new GetStudentAttendanceResponse
                {
                    Username = s.User.UserName!,
                    FullName = s.User.FullName.ToString(),
                    StartBalance = finance.StartBalance,
                    Expenses = finance.Expenses,
                    Payments = finance.Payments,
                    EndBalance = finance.EndBalance,
                    Attendance = attendance?.Select(a => a.ToDto()).ToList() ?? []
                };
            })
            .ToList();
    }

    public static AttendanceMarkDto ToDto(this AttendanceMark attendanceMark)
    {
        return new AttendanceMarkDto
        {
            PracticeId = attendanceMark.PracticeId,
            PracticeStart = attendanceMark.PracticeTime
        };
    }
}