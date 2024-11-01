namespace TrainerJournal.Application.Services.Schedule.Dtos;

public record PracticeDto(
    Guid Id,
    DateTime Start,
    DateTime End,
    PracticeGroupDto Group,
    string PracticeType,
    float Price);