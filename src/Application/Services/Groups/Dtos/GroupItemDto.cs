using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Groups.Dtos;

public class GroupItemDto
{
    public Guid Id { get; init; }
    
    [Required]
    [DefaultValue("Группа")]
    public string Name { get; init; }

    [Required]
    [DefaultValue("address")]
    public string HallAddress { get; init; }
    
    [Required]
    [DefaultValue("#A293FF")]
    public string HexColor { get; init; }
    
    public int StudentsCount { get; init; }
    
    public float Price { get; init; }
}