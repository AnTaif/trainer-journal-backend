namespace TrainerJournal.Application.Services.Practices.Dtos.Requests;

public record CreatePracticeItemRequest(
    DateTime StartTime,
    DateTime EndTime);