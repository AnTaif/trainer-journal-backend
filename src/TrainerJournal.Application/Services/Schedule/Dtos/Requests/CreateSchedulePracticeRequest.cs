namespace TrainerJournal.Application.Services.Schedule.Dtos.Requests;

public class CreateSchedulePracticeRequest(DateTime start, DateTime end)
{
    public DateTime Start { get; init; } = start;
    public DateTime End { get; init; } = end;
}