using ErrorOr;
using Microsoft.Extensions.Logging;
using TrainerJournal.Application.Services.Groups;
using TrainerJournal.Application.Services.Students.Dtos;
using TrainerJournal.Application.Services.Students.Dtos.Requests;
using TrainerJournal.Application.Services.Students.Dtos.Responses;
using TrainerJournal.Application.Services.Users;
using TrainerJournal.Application.Services.Users.Dtos.Requests;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.Students;

public class StudentService(
    IUserService userService,
    IStudentRepository studentRepository,
    IGroupRepository groupRepository,
    ILogger<StudentService> logger) : IStudentService
{
    public async Task<ErrorOr<CreateStudentResponse>> CreateAsync(CreateStudentRequest request, Guid groupId)
    {
        var userResult = await userService.CreateAsync(
            new CreateUserRequest(request.FullName, request.Email, request.Phone, request.Gender));

        if (userResult.IsError)
            return userResult.FirstError;

        var user = userResult.Value;
        
        var student = new Student(user.Id, request.BirthDate.ToUniversalTime(), request.SchoolGrade, request.Kyu,
            request.Address, request.FirstParentInfo, request.SecondParentInfo);
        
        AddToGroup(student, groupId);
        
        studentRepository.AddStudent(student);
        await studentRepository.SaveChangesAsync();

        return new CreateStudentResponse(student.Id, user.Username, user.Password, student.User.FullName.ToString());
    }

    public async Task<ErrorOr<List<StudentItemDto>>> GetStudentsByGroupAsync(Guid groupId, Guid userId)
    {
        var group = await groupRepository.GetByIdAsync(groupId);
        if (group == null)
        {
            logger.LogWarning("Group not found by id: {id}", groupId);
            return Error.NotFound(description: "Group not found");
        }

        if (group.TrainerId != userId)
        {
            var student = await studentRepository.GetByUserIdAsync(userId);
            if (student == null || student.GroupId == groupId)
            {
                logger.LogWarning("User {userId} don't have access to group {groupId}", userId, groupId);
                return Error.Forbidden(description: "You don't have access to this group");
            }
        }
        
        var students = await studentRepository.GetAllByGroupIdAsync(groupId);
        return students.Select(s => s.ToItemDto()).ToList();
    }

    private void AddToGroup(Student student, Guid groupId)
    {
        //TODO: add groupId validation
        student.ChangeGroup(groupId);
    }
}