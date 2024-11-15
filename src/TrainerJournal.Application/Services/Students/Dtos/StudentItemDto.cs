using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Students.Dtos;

public class StudentItemDto
{
    public string Username { get; init; }
    
    [Required]
    [DefaultValue("Фамилия Имя Отчество")]
    public string FullName { get; init; }
    
    public float Balance { get; init; }
    
    public int Age { get; init; }
    
    public int SchoolGrade { get; init; }
    
    public int? Kyu { get; init; }

    public string Gender { get; init; }
    
    public List<Guid> GroupIds { get; init; }
    
    public ContactDto Contact { get; init; }
}