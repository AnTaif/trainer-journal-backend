using TrainerJournal.Application.DataAnnotations;

namespace TrainerJournal.Application.Students.Dtos.Requests;

public record CreateStudentRequest(
    Guid GroupId,
    [MinimumWordsCount(2)]
    string FullName, 
    [GenderEnum]
    string Gender,
    DateTime BirthDate, 
    int SchoolGrade, 
    int AikidoGrade,
    string Address,
    string? Email = null,
    string? Phone = null,
    string? FirstParentName = null, 
    string? FirstParentContact = null, 
    string? SecondParentName = null, 
    string? SecondParentContact = null,
    string? TelegramUsername = null);