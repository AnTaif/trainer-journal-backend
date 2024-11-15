using TrainerJournal.Application.DataAnnotations;

namespace TrainerJournal.Application.Services.Practices.Dtos.Requests;

public class ChangePracticeRequest
{
    /// <summary>
    /// Время начала практики в текущей неделе
    /// </summary>
    public DateTime PracticeStart { get; init; }
    
    public Guid? GroupId { get; init; }
    public DateTime? NewStart { get; init; }
    public DateTime? NewEnd { get; init; }

    public string? HallAddress { get; } = null!;

    [PracticeEnum]
    public string? PracticeType { get; init; }
    
    public float? Price { get; init; }
}