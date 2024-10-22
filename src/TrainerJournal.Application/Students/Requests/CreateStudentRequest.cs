namespace TrainerJournal.Application.Students.Requests;

public record CreateStudentRequest(
    string FullName, 
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