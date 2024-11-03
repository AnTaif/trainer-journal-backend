using TrainerJournal.Domain.Common;

namespace TrainerJournal.Application.Services.Students.Dtos;

public record StudentInfoDto(
    Guid? GroupId,
    float Balance,
    DateTime BirthDate,
    int? SchoolGrade,
    int? Kyu,
    DateTime? KyuUpdatedAt,
    DateTime TrainingStartDate,
    string? Address,
    ParentInfoDto FirstParentInfo,
    ParentInfoDto SecondParentInfo);