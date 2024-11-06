using TrainerJournal.Application.DataAnnotations;

namespace TrainerJournal.Application.Services.Practices.Dtos.Requests;

public class ChangePracticeRequest(
    DateTime currentStart, 
    Guid? groupId, 
    DateTime? newStart, 
    DateTime? newEnd, 
    string? practiceType, 
    float? price)
{
    /// <summary>
    /// Время начала практики в текущей неделе
    /// </summary>
    public DateTime currentStart { get; init; } = currentStart;
    
    public Guid? GroupId { get; init; } = groupId;
    public DateTime? NewStart { get; init; } = newStart;
    public DateTime? NewEnd { get; init; } = newEnd;
    
    [PracticeEnum]
    public string? PracticeType { get; init; } = practiceType;
    
    public float? Price { get; init; } = price;
}