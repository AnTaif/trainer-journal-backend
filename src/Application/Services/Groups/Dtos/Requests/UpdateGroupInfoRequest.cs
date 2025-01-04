using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Groups.Dtos.Requests;

public class UpdateGroupInfoRequest
{
    public string? Name { get; init; }

    public float? Price { get; init; }

    public string? HallAddress { get; init; }
    
    [RegularExpression("^#[a-zA-Z0-9]{6}$")]
    public string? HexColor { get; init; }
}