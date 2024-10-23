using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TrainerJournal.Application.Students.Dtos.Requests;
using TrainerJournal.Application.Students.Dtos.Responses;
using TrainerJournal.Application.Users;
using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Enums.Gender;

namespace TrainerJournal.Application.Students;

public class StudentService(
    UserManager<User> userManager, 
    IStudentRepository studentRepository,
    ILogger<StudentService> logger) : IStudentService
{
    public async Task<ErrorOr<CreateStudentResponse>> CreateAsync(CreateStudentRequest request, Guid groupId)
    {
        //TODO: move user creation to the UserService
        var username = UserService.GenerateUsername(request.FullName, userManager);
        var password = UserService.GeneratePassword();
        
        var existedUser = await userManager.FindByNameAsync(username);
        if (existedUser != null)
        {
            logger.LogWarning("User with username {username} already exists", username);
            return Error.Failure("Student.Create", "User is already exists");
        }

        var user = new User(username, request.Email, request.Phone, request.FullName, request.Gender.ToGenderEnum(),
            request.TelegramUsername);

        var result = await userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            logger.LogError("Failed to create user: {username}", username);
            return Error.Failure("User not created");
        }

        await userManager.AddToRoleAsync(user, RoleConstants.User);
        
        var student = new Student(user.Id, request.BirthDate.ToUniversalTime(), request.SchoolGrade, request.AikidoGrade,
            request.Address, request.FirstParentName, request.FirstParentContact, request.SecondParentName,
            request.SecondParentContact);
        
        AddToGroup(student, groupId);
        
        studentRepository.AddStudent(student);
        await studentRepository.SaveChangesAsync();

        return new CreateStudentResponse(student.Id, username, password, student.User.GetFullName());
    }

    private void AddToGroup(Student student, Guid groupId)
    {
        //TODO: add groupId validation
        student.ChangeGroup(groupId);
    }
}