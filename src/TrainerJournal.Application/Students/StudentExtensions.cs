using TrainerJournal.Application.Students.Dtos;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Students;

public static class StudentExtensions
{
    public static StudentInfoDto ToInfoDto(this Student student)
    {
        return new StudentInfoDto(student.Id, student.GroupId, student.Balance, student.BirthDate, student.SchoolGrade,
            student.AikidoGrade, student.LastAikidoGradeDate, student.TrainingStartDate, student.Address,
            student.firstParentName, student.firstParentContact, student.secondParentName, student.secondParentContact);
    }
}