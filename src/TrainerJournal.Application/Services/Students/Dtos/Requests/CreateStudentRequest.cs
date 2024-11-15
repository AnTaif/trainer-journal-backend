using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TrainerJournal.Application.DataAnnotations;

namespace TrainerJournal.Application.Services.Students.Dtos.Requests;

public class CreateStudentRequest
{
    [MinimumWordsCount(2)]
    [Required]
    [DefaultValue("Фамилия Имя Отчество")]
    public string FullName { get; init; } = null!;

    [Required]
    [DefaultValue("М")]
    [GenderEnum]
    public string Gender { get; init; } = null!;
    
    public DateTime BirthDate { get; init; }
    
    [Range(0, 11)]
    [DefaultValue(0)]
    public int SchoolGrade { get; init; }
    
    [Range(1, 10)]
    public int? Kyu { get; init; }
    
    [Required]
    [DefaultValue("address")]
    public string? Address { get; init; }

    public List<Guid> GroupIds { get; init; } = null!;

    [Length(1, 2)] 
    public List<ContactDto> Contacts { get; init; } = null!;
}