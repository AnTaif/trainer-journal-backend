namespace TrainerJournal.Application.Services.Practices.Dtos.Requests;

public record CreateScheduleRequest(
    float Price,
    int RepeatWeeks,
    List<CreatePracticeItemRequest> Practices);