using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Students.Dtos;

public class StudentItemDto(
    Guid id,
    string fullName,
    Guid? groupId,
    float balance,
    DateTime birthDate,
    int age,
    int schoolGrade,
    int? kyu)
{
    public Guid Id { get; init; } = id;
    
    [Required]
    [DefaultValue("Фамилия Имя Отчество")]
    public string FullName { get; init; } = fullName;
    
    public Guid? GroupId { get; init; } = groupId;
    public float Balance { get; init; } = balance;
    public DateTime BirthDate { get; init; } = birthDate;
    public int Age { get; init; } = age;
    public int SchoolGrade { get; init; } = schoolGrade;
    public int? Kyu { get; init; } = kyu;
}