namespace TrainerJournal.Application.Services.Practices.Dtos;

public record PracticeItemDto(Guid Id, string GroupName, DateTime StartDate, DateTime EndDate);