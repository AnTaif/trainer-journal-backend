using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Groups.Dtos.Requests;

public class CreateGroupRequest(string name, string? hallAddress, string? hexColor)
{
    [Required]
    [DefaultValue("name")]
    public string Name { get; init; } = name;

    public string HallAddress { get; init; } = hallAddress ?? "";
    
    [RegularExpression("^#[a-zA-Z0-9]{6}$")]
    public string? HexColor { get; init; } = hexColor;
}