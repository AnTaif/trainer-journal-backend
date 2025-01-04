namespace TrainerJournal.Application.Services.Schedules.Dtos.Requests;

public class CreateScheduleRequest(
    DateTime startDay,
    DateTime? until,
    List<CreateSchedulePracticeRequest> practices)
{
    public DateTime StartDay { get; init; } = startDay.ToUniversalTime();
    public DateTime? Until { get; init; } = until?.ToUniversalTime();
    public List<CreateSchedulePracticeRequest> Practices { get; init; } = practices;
}