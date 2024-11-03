using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Students.Dtos.Requests;

public record UpdateStudentInfoRequest(
    DateTime? BirthDate, 
    [Range(0, 11)]
    int? SchoolGrade, 
    string? Address, 
    int? Kyu,
    List<ExtraContactDto>? ExtraContacts);