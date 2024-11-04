namespace TrainerJournal.Application.Services.Schedule.Dtos.Requests;

public class CreateScheduleRequest(
    Guid groupId,
    DateTime startDay,
    DateTime? until,
    List<CreateSchedulePracticeRequest> practices)
{
    public Guid GroupId { get; init; } = groupId;
    public DateTime StartDay { get; init; } = startDay;
    public DateTime? Until { get; init; } = until;
    public List<CreateSchedulePracticeRequest> Practices { get; init; } = practices;
}