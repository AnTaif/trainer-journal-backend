using TrainerJournal.Application.Services.Students.Dtos;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.Students;

public static class StudentExtensions
{
    public static StudentInfoDto ToInfoDto(this Student student)
    {
        var firstParentInfo = student.firstParentName != null 
            ? new ParentInfo(student.firstParentName, student.firstParentContact ?? "") 
            : null;
        var secondParentInfo = student.secondParentName != null
            ? new ParentInfo(student.secondParentName, student.secondParentContact ?? "")
            : null;
        
        return new StudentInfoDto(student.GroupId, student.Balance, student.BirthDate, student.SchoolGrade,
            student.Kyu, student.KyuUpdatedAt, student.TrainingStartDate, student.Address,
           firstParentInfo, secondParentInfo);
    }

    public static StudentItemDto ToItemDto(this Student student)
    {
        return new StudentItemDto(student.Id, student.User.FullName.ToString(), student.GroupId, student.Balance, 
            student.BirthDate, GetYearsSince(student.BirthDate), student.SchoolGrade, student.Kyu);
    }

    private static int GetYearsSince(DateTime a)
    {
        var zeroTime = new DateTime(1, 1, 1);
        var span = DateTime.UtcNow - a;
        return (zeroTime + span).Year - 1;
    }
}