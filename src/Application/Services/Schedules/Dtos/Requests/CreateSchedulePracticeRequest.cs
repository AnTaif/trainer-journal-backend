namespace TrainerJournal.Application.Services.Schedules.Dtos.Requests;

public class CreateSchedulePracticeRequest(DateTime start, DateTime end)
{
    public DateTime Start { get; init; } = start.ToUniversalTime();
    public DateTime End { get; init; } = end.ToUniversalTime();
}