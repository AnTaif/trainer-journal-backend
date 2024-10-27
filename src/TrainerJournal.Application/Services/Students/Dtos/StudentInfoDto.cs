namespace TrainerJournal.Application.Services.Students.Dtos;

//TODO(!): replace GroupId with GroupInfoDto
public record StudentInfoDto(
    Guid GroupId,
    float Balance,
    DateTime BirthDate,
    int SchoolGrade,
    int Kyu,
    DateTime KyuUpdatedAt,
    DateTime TrainingStartDate,
    string Address,
    ParentInfo? FirstParentInfo,
    ParentInfo? SecondParentInfo
    );