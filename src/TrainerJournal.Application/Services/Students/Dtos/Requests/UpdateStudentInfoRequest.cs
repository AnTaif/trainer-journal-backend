using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Students.Dtos.Requests;

public class UpdateStudentInfoRequest(DateTime? birthDate, int? schoolGrade, string? address, int? kyu, List<ContactDto>? contacts)
{
    public DateTime? BirthDate { get; init; } = birthDate;
    
    [Range(0, 11)]
    public int? SchoolGrade { get; init; } = schoolGrade;
    
    public string? Address { get; init; } = address;
    
    [Range(1, 12)]
    public int? Kyu { get; init; } = kyu;
    
    [DefaultValue(null)]
    [Length(1, 2)]
    public List<ContactDto>? Contacts { get; init; } = contacts;
}