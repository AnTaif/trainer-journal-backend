namespace TrainerJournal.Application.Services.Schedule.Dtos.Requests;

public record CreateSinglePracticeRequest(
    Guid GroupId,
    DateTime Start,
    DateTime End,
    string PracticeType,
    float Price,
    Guid HallId);