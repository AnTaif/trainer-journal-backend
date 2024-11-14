using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Groups.Dtos;

public class GroupItemDto(Guid id, string name, string hexColor, int studentsCount, float price)
{
    public Guid Id { get; init; } = id;
    
    [Required]
    [DefaultValue("Группа")]
    public string Name { get; init; } = name;
    
    [Required]
    [DefaultValue("#A293FF")]
    public string HexColor { get; init; } = hexColor;
    
    public int StudentsCount { get; init; } = studentsCount;
    
    public float Price { get; init; } = price;
}