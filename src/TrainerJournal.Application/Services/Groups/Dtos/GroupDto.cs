namespace TrainerJournal.Application.Services.Groups.Dtos;

public record GroupDto(
    Guid Id,
    string Name, 
    int StudentsCount,
    Guid TrainerId,
    string HexColor);