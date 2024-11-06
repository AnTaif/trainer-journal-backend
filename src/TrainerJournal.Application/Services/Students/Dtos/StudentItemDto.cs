using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Students.Dtos;

public class StudentItemDto(
    Guid id,
    string fullName,
    Guid? groupId,
    float balance,
    int age,
    int schoolGrade,
    int? kyu,
    ExtraContactDto contact)
{
    public Guid Id { get; init; } = id;
    public Guid? GroupId { get; init; } = groupId;
    
    [Required]
    [DefaultValue("Фамилия Имя Отчество")]
    public string FullName { get; init; } = fullName;
    
    public float Balance { get; init; } = balance;
    public int Age { get; init; } = age;
    public int SchoolGrade { get; init; } = schoolGrade;
    public int? Kyu { get; init; } = kyu;
    public ExtraContactDto Contact { get; } = contact;
}