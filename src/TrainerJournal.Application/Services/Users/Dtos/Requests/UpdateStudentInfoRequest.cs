using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TrainerJournal.Application.Services.Students.Dtos;

namespace TrainerJournal.Application.Services.Users.Dtos.Requests;

public class UpdateStudentInfoRequest
{
    public DateTime? BirthDate { get; set; }
    
    [Range(0, 11)]
    public int? SchoolGrade { get; set; }
    
    public string? Address { get; set; }
    
    [Range(1, 12)]
    public int? Kyu { get; set; }
    
    [DefaultValue(null)]
    [Length(1, 2)]
    public IReadOnlyCollection<ContactDto>? Contacts { get; set; }
}