using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Groups.Dtos.Requests;

public class UpdateGroupInfoRequest(string? name, string? hexColor)
{
    public string? Name { get; init; } = name;
    
    [RegularExpression("^#[a-zA-Z0-9]{6}$")]
    public string? HexColor { get; init; } = hexColor;
}