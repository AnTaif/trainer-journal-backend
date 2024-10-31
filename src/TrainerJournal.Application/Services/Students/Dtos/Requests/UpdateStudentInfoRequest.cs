namespace TrainerJournal.Application.Services.Students.Dtos.Requests;

public record UpdateStudentInfoRequest(
    DateTime? BirthDate, 
    int? SchoolGrade, 
    string? Address, 
    ParentInfoDto? FirstParentInfo, 
    ParentInfoDto? SecondParentInfo);