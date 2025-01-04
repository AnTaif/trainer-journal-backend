using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Students.Dtos;

public class StudentItemDto
{
    public string Username { get; init; } = null!;

    [Required]
    [DefaultValue("Фамилия Имя Отчество")]
    public string FullName { get; init; } = null!;
    
    public float Balance { get; init; }
    
    public DateTime BirthDate { get; set; }
    
    public int Age { get; init; }
    
    public int SchoolGrade { get; init; }
    
    public int? Kyu { get; init; }

    public string Gender { get; init; } = null!;
    
    [Required]
    [DefaultValue("address")]
    public string Address { get; init; } = null!;

    public List<Guid> GroupIds { get; init; } = null!;

    public ContactDto Contact { get; init; } = null!;
}