using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Students.Dtos;

public class StudentShortDto
{
    public string Username { get; init; }
    
    [Required]
    [DefaultValue("Фамилия Имя Отчество")]
    public string FullName { get; init; }
    
    public float Balance { get; init; }
    
    public List<Guid> GroupIds { get; init; }
}