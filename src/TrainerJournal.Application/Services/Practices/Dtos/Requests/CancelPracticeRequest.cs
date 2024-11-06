namespace TrainerJournal.Application.Services.Practices.Dtos.Requests;

public class CancelPracticeRequest(DateTime practiceStart, string? comment)
{
    public DateTime PracticeStart { get; init; } = practiceStart;

    public string Comment { get; init; } = comment ?? "";
}