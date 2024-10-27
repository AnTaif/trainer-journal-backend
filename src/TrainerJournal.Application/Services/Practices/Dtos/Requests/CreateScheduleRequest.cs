namespace TrainerJournal.Application.Services.Practices.Dtos.Requests;

public record CreateScheduleRequest(
    float Price,
    int RepeatInWeeks,
    List<CreatePracticeItemRequest> Practices);