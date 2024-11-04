using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Practices.Dtos;

public class PracticeGroupDto(Guid id, string name)
{
    public Guid Id { get; init; } = id;
    
    [Required]
    [DefaultValue("Команда 1")]
    public string Name { get; init; } = name;
}