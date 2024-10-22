namespace TrainerJournal.Application.Students.Responses;

public record CreateStudentResponse(Guid Id, string FirstName, string LastName, string? MiddleName, string? Username);