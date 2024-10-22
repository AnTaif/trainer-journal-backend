namespace TrainerJournal.Application.Students.Dtos.Responses;

public record CreateStudentResponse(Guid Id, string Username, string Password, string FullName);