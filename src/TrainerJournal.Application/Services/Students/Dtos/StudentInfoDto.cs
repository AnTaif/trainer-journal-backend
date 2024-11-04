using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TrainerJournal.Application.Services.Students.Dtos;

public class StudentInfoDto(
    Guid? groupId,
    float balance,
    DateTime birthDate,
    int schoolGrade,
    int? kyu,
    DateTime? kyuUpdatedAt,
    DateTime trainingStartDate,
    string address,
    List<ExtraContactDto> extraContacts)
{
    public Guid? GroupId { get; init; } = groupId;
    public float Balance { get; init; } = balance;
    public DateTime BirthDate { get; init; } = birthDate;
    public int SchoolGrade { get; init; } = schoolGrade;
    public int? Kyu { get; init; } = kyu;
    public DateTime? KyuUpdatedAt { get; init; } = kyuUpdatedAt;
    public DateTime TrainingStartDate { get; init; } = trainingStartDate;
    
    [Required]
    [DefaultValue("address")]
    public string Address { get; init; } = address;
    
    public List<ExtraContactDto> ExtraContacts { get; init; } = extraContacts;
}