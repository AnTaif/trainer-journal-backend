using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Groups.Dtos;

public class GroupDto
{
    public Guid Id { get; init; }
    
    [Required]
    [DefaultValue("name")]
    public string Name { get; init; }
    
    public int StudentsCount { get; init; }
    
    public Guid TrainerId { get; init; }

    [Required]
    [DefaultValue("address")]
    public string HallAddress { get; init; }
    
    [Required]
    [DefaultValue("#A293FF")]
    public string HexColor { get; init; }
    
    public float Price { get; init; }
}