using ErrorOr;
using Microsoft.Extensions.Logging;
using TrainerJournal.Application.Students.Dtos.Requests;
using TrainerJournal.Application.Students.Dtos.Responses;
using TrainerJournal.Application.Users;
using TrainerJournal.Application.Users.Dtos.Requests;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Students;

public class StudentService(
    IUserService userService,
    IStudentRepository studentRepository,
    ILogger<StudentService> logger) : IStudentService
{
    public async Task<ErrorOr<CreateStudentResponse>> CreateAsync(CreateStudentRequest request)
    {
        var userResult = await userService.CreateAsync(
            new CreateUserRequest(request.FullName, request.Email, request.Phone, request.Gender, request.TelegramUsername));

        if (userResult.IsError)
            return userResult.FirstError;

        var user = userResult.Value;
        
        var student = new Student(user.Id, request.BirthDate.ToUniversalTime(), request.SchoolGrade, request.AikidoGrade,
            request.Address, request.FirstParentName, request.FirstParentContact, request.SecondParentName,
            request.SecondParentContact);
        
        AddToGroup(student, request.GroupId);
        
        studentRepository.AddStudent(student);
        await studentRepository.SaveChangesAsync();

        return new CreateStudentResponse(student.Id, user.UserName, user.Password, student.User.GetFullName());
    }

    private void AddToGroup(Student student, Guid groupId)
    {
        //TODO: add groupId validation
        student.ChangeGroup(groupId);
    }
}