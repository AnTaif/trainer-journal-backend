namespace TrainerJournal.Application.Services.Groups.Dtos;

public record GroupItemDto(
    Guid Id, 
    string Name, 
    string HexColor, 
    int StudentsCount);