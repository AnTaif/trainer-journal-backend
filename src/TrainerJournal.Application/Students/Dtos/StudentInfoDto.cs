namespace TrainerJournal.Application.Students.Dtos;

//TODO(!): replace GroupId with GroupInfoDto
public record StudentInfoDto(
    Guid GroupId,
    float Balance,
    DateTime BirthDate,
    int SchoolGrade,
    int AikidoGrade,
    DateTime LastAikidoGradeDate,
    DateTime TrainingStartDate,
    string Address,
    string? FirstParentName,
    string? FirstParentContact,
    string? SecondParentName,
    string? SecondParentContact
    );