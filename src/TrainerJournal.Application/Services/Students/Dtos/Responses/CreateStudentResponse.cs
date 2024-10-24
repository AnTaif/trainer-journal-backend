namespace TrainerJournal.Application.Services.Students.Dtos.Responses;

public record CreateStudentResponse(Guid Id, string Username, string Password, string FullName);