using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Groups.Dtos;

public class GroupDto(Guid id, string name, int studentsCount, Guid trainerId, string hexColor, float price)
{
    public Guid Id { get; init; } = id;
    
    [Required]
    [DefaultValue("name")]
    public string Name { get; init; } = name;
    public int StudentsCount { get; init; } = studentsCount;
    public Guid TrainerId { get; init; } = trainerId;
    
    [Required]
    [DefaultValue("#A293FF")]
    public string HexColor { get; init; } = hexColor;
    
    public float Price { get; init; } = price;
}