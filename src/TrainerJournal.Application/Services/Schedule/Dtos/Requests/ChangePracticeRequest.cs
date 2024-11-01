namespace TrainerJournal.Application.Services.Schedule.Dtos.Requests;

public record ChangePracticeRequest(
    Guid? GroupId,
    DateTime? Start,
    DateTime? End,
    string? PracticeType,
    float? Price,
    Guid? HallId);