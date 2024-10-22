using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TrainerJournal.Application.Students.Requests;
using TrainerJournal.Application.Students.Responses;
using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Enums.Gender;
using TrainerJournal.Domain.Exceptions;

namespace TrainerJournal.Application.Students;

public class StudentService(
    UserManager<User> userManager, 
    IStudentRepository studentRepository,
    ILogger<StudentService> logger) : IStudentService
{
    public async Task<CreateStudentResponse> CreateAsync(CreateStudentRequest request, Guid groupId)
    {
        var username = Guid.NewGuid().ToString(); // TODO: add auto-generation of the Username
        var password = Guid.NewGuid().ToString(); // TODO: add auto-generation of the Password
        
        var existedUser = await userManager.FindByNameAsync(username);
        if (existedUser != null)
        {
            logger.LogWarning("User with username {username} already exists", username);            
            throw new BadRequestException("User is already exists");
        }

        var user = new User(username, request.Email, request.Phone, request.FullName, request.Gender.ToGenderEnum(),
            request.TelegramUsername);

        var result = await userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            logger.LogError("Failed to create user: {username}", username);
            throw new BadRequestException("User not created");
        }

        await userManager.AddToRoleAsync(user, RoleConstants.User);
        
        var student = new Student(user.Id, request.BirthDate.ToUniversalTime(), request.SchoolGrade, request.AikidoGrade,
            request.Address, request.FirstParentName, request.FirstParentContact, request.SecondParentName,
            request.SecondParentContact);
        
        AddToGroup(student, groupId);
        
        studentRepository.AddStudent(student);
        await studentRepository.SaveChangesAsync();

        return new CreateStudentResponse(student.Id, student.User.FirstName, student.User.LastName,
            student.User.MiddleName, student.User.UserName);
    }

    private void AddToGroup(Student student, Guid groupId)
    {
        //TODO: add groupId validation
        student.ChangeGroup(groupId);
    }
}