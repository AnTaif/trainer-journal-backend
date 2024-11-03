using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Groups.Dtos.Requests;

public record UpdateGroupInfoRequest(
    string? Name, 
    [Description("Color in format: #ffffff")]
    [RegularExpression("^#[a-zA-Z0-9]{6}$")]
    string? HexColor);