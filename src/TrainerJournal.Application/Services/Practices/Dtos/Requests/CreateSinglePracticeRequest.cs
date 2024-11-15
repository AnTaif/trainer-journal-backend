using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TrainerJournal.Application.DataAnnotations;

namespace TrainerJournal.Application.Services.Practices.Dtos.Requests;

public class CreateSinglePracticeRequest
{
    public Guid GroupId { get; init; }
    public DateTime Start { get; init; }
    public DateTime End { get; init; }
    
    [Required]
    [PracticeEnum]
    [DefaultValue("Семинар")]
    public string PracticeType { get; init; }
    
    public string? HallAddress { get; init; }
    
    [Range(0.0, float.MaxValue)]
    public float? Price { get; init; }
}