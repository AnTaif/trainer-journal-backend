using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Attendance.Dtos.Responses;

public class GetPracticeAttendanceResponse
{
    [Required]
    [DefaultValue("login")]
    public string Username { get; init; }
    
    [Required]
    [DefaultValue("Фамилия Имя Отчество")]
    public string FullName { get; init; }
    
    public bool IsMarked { get; init; }
}