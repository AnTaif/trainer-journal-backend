using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Practices.Dtos;

public class PracticeGroupDto
{
    public Guid Id { get; init; }
    
    [Required]
    [DefaultValue("Команда 1")]
    public string Name { get; init; }
}