using TrainerJournal.Application.Services.Students.Dtos;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.Students;

public static class StudentExtensions
{
    public static StudentInfoDto ToInfoDto(this Student student)
    {
        return new StudentInfoDto(student.GroupId, student.Balance, student.BirthDate, student.SchoolGrade,
            student.Kyu, student.KyuUpdatedAt, student.TrainingStartDate, student.Address,
           student.ExtraContacts.Select(e => e.ToDto()).ToList());
    }

    public static StudentItemDto ToItemDto(this Student student)
    {
        return new StudentItemDto(student.Id, student.User.FullName.ToString(), student.GroupId, student.Balance, 
            GetYearsSince(student.BirthDate), student.SchoolGrade, student.Kyu, student.ExtraContacts.First().ToDto());
    }

    private static int GetYearsSince(DateTime a)
    {
        var zeroTime = new DateTime(1, 1, 1);
        var span = DateTime.UtcNow - a;
        return (zeroTime + span).Year - 1;
    }
}