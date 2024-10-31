using TrainerJournal.Application.DataAnnotations;
using TrainerJournal.Domain.Common;

namespace TrainerJournal.Application.Services.Students.Dtos.Requests;

public record CreateStudentRequest(
    [MinimumWordsCount(2)]
    string FullName, 
    [GenderEnum]
    string Gender,
    DateTime BirthDate, 
    int SchoolGrade, 
    int Kyu,
    string Address,
    string? Email = null,
    string? Phone = null,
    ParentInfo? FirstParentInfo = null, 
    ParentInfo? SecondParentInfo = null);