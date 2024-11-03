namespace TrainerJournal.Application.Services.Students.Dtos;

public record StudentItemDto(
    Guid Id,
    string FullName,
    Guid? GroupId,
    float Balance,
    DateTime BirthDate,
    int Age,
    int SchoolGrade,
    int AikidoGrade
    );