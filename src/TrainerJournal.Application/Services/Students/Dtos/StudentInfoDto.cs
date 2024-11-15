using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Students.Dtos;

public class StudentInfoDto
{
    public DateTime BirthDate { get; set; }
    
    public int Age { get; set; }
    
    public int SchoolGrade { get; init; }
    
    public int? Kyu { get; init; }
    
    public DateTime? KyuUpdatedAt { get; init; }
    
    public DateTime TrainingStartDate { get; init; }
    
    [Required]
    [DefaultValue("address")]
    public string Address { get; init; }
    
    public float Balance { get; init; }
    
    public List<ContactDto> Contacts { get; init; }
}