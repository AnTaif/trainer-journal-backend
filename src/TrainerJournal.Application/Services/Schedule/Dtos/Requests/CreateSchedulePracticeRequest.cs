namespace TrainerJournal.Application.Services.Schedule.Dtos.Requests;

public record CreateSchedulePracticeRequest(
    DateTime Start,
    DateTime End);