using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Groups.Dtos;

public class GroupItemDto(Guid id, string name, string hexColor, int studentsCount)
{
    public Guid Id { get; init; } = id;
    
    [Required]
    [DefaultValue("name")]
    public string Name { get; init; } = name;
    
    [Required]
    [DefaultValue("#A293FF")]
    public string HexColor { get; init; } = hexColor;
    
    public int StudentsCount { get; init; } = studentsCount;
}