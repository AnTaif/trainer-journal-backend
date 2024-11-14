using TrainerJournal.Application.Services.Students.Dtos;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Enums.Gender;

namespace TrainerJournal.Application.Services.Students;

public static class StudentExtensions
{
    public static StudentInfoDto ToInfoDto(this Student student)
    {
        return new StudentInfoDto(student.Balance, student.BirthDate, student.SchoolGrade,
            student.Kyu, student.KyuUpdatedAt, student.TrainingStartDate, student.Address,
            student.Groups.Select(g => g.Id).ToList(),
           student.Contacts.Select(e => e.ToDto()).ToList());
    }

    public static StudentItemDto ToItemDto(this Student student)
    {
        return new StudentItemDto(
            student.UserId, 
            student.User.FullName.ToString(),
            student.Balance, 
            GetYearsSince(student.BirthDate), student.SchoolGrade, student.Kyu, 
            student.User.Gender.ToGenderString(),
            student.Groups.Select(g => g.Id).ToList(), 
            student.Contacts.First().ToDto());
    }

    private static int GetYearsSince(DateTime a)
    {
        var zeroTime = new DateTime(1, 1, 1);
        var span = DateTime.UtcNow - a;
        return (zeroTime + span).Year - 1;
    }
}