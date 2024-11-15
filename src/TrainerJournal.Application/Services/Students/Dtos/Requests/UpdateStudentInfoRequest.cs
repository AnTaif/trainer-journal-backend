using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Students.Dtos.Requests;

public class UpdateStudentInfoRequest
{
    [Range(0, 11)]
    public int? SchoolGrade { get; init; }
    
    public string? Address { get; init; }
    
    [Range(1, 12)]
    public int? Kyu { get; init; }
    
    public DateTime? BirthDate { get; init; }
    
    [DefaultValue(null)]
    [Length(1, 2)]
    public IReadOnlyCollection<ContactDto>? Contacts { get; init; }
}