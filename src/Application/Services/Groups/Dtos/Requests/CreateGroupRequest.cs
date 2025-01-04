using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Groups.Dtos.Requests;

public class CreateGroupRequest
{
    [Required]
    [DefaultValue("name")]
    public string Name { get; init; }
    
    public float Price { get; init; }

    [Required]
    [DefaultValue("address")]
    public string? HallAddress { get; init; }
    
    [RegularExpression("^#[a-zA-Z0-9]{6}$")]
    public string? HexColor { get; init; }
}