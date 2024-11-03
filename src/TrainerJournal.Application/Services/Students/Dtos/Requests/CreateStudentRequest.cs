using System.ComponentModel.DataAnnotations;
using TrainerJournal.Application.DataAnnotations;

namespace TrainerJournal.Application.Services.Students.Dtos.Requests;

public record CreateStudentRequest(
    [MinimumWordsCount(2)]
    string FullName, 
    [GenderEnum]
    string Gender,
    DateTime BirthDate, 
    [Range(0, 11)]
    int SchoolGrade, 
    [Range(1, 10)]
    int? Kyu,
    string? Address,
    List<CreateExtraContactRequest> ExtraContacts);