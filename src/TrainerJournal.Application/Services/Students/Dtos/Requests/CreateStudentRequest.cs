using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TrainerJournal.Application.DataAnnotations;

namespace TrainerJournal.Application.Services.Students.Dtos.Requests;

public class CreateStudentRequest(string fullName, string gender, DateTime birthDate, int schoolGrade, int? kyu, string? address, List<CreateExtraContactRequest> extraContacts)
{
    [MinimumWordsCount(2)]
    [Required]
    [DefaultValue("Фамилия Имя Отчество")]
    public string FullName { get; init; } = fullName;
    
    [Required]
    [DefaultValue("М")]
    [GenderEnum]
    public string Gender { get; init; } = gender;
    
    public DateTime BirthDate { get; init; } = birthDate;
    
    [Range(0, 11)]
    [DefaultValue(0)]
    public int SchoolGrade { get; init; } = schoolGrade;
    
    [Range(1, 10)]
    public int? Kyu { get; init; } = kyu;
    
    [Required]
    [DefaultValue("address")]
    public string? Address { get; init; } = address;
    
    public List<CreateExtraContactRequest> ExtraContacts { get; init; } = extraContacts;
}