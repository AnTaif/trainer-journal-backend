using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TrainerJournal.Application.DataAnnotations;

namespace TrainerJournal.Application.Services.Practices.Dtos.Requests;

public class CreateSinglePracticeRequest(Guid groupId, DateTime start, DateTime end, string practiceType, float? price)
{
    public Guid GroupId { get; init; } = groupId;
    public DateTime Start { get; init; } = start;
    public DateTime End { get; init; } = end;
    
    [Required]
    [PracticeEnum]
    [DefaultValue("Семинар")]
    public string PracticeType { get; init; } = practiceType;
    
    [Range(0.0, float.MaxValue)]
    public float? Price { get; init; } = price;
}