using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Groups.Dtos.Requests;

public class UpdateGroupInfoRequest(string? name, float? price, string? hallAddress, string? hexColor)
{
    public string? Name { get; init; } = name;

    public float? Price { get; init; } = price;

    public string? HallAddress { get; init; } = hallAddress;
    
    [RegularExpression("^#[a-zA-Z0-9]{6}$")]
    public string? HexColor { get; init; } = hexColor;
}