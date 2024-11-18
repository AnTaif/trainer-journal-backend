using TrainerJournal.Application.Services.Students.Dtos;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Enums.Gender;

namespace TrainerJournal.Application.Services.Students;

public static class StudentExtensions
{
    public static StudentInfoDto ToInfoDto(this Student student)
    {
        return new StudentInfoDto
        {
            BirthDate = student.BirthDate,
            Age = GetYearsSince(student.BirthDate),
            SchoolGrade = student.SchoolGrade,
            Kyu = student.Kyu,
            KyuUpdatedAt = student.KyuUpdatedAt,
            TrainingStartDate = student.TrainingStartDate,
            Address = student.Address,
            Balance = student.Balance,
            Contacts = student.Contacts.Select(e => e.ToDto()).ToList()
        };
    }

    public static StudentItemDto ToItemDto(this Student student)
    {
        return new StudentItemDto
        {
            Username = student.User.UserName!,
            FullName = student.User.FullName.ToString(),
            Balance = student.Balance,
            BirthDate = student.BirthDate,
            Age = GetYearsSince(student.BirthDate),
            SchoolGrade = student.SchoolGrade,
            Kyu = student.Kyu,
            Gender = student.User.Gender.ToGenderString(),
            Address = student.Address,
            GroupIds = student.Groups.Select(g => g.Id).ToList(),
            Contact = student.Contacts.First().ToDto()
        };
    }

    private static int GetYearsSince(DateTime date)
    {
        var years = DateTime.UtcNow.Year - date.Year;

        if (DateTime.UtcNow < date.AddYears(years))
        {
            years--;
        }

        return years;
    }
}