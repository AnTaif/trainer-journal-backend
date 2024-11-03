using ErrorOr;
using TrainerJournal.Application.Services.Groups;
using TrainerJournal.Application.Services.Students.Dtos;
using TrainerJournal.Application.Services.Students.Dtos.Requests;
using TrainerJournal.Application.Services.Students.Dtos.Responses;
using TrainerJournal.Application.Services.Users;
using TrainerJournal.Application.Services.Users.Dtos.Requests;
using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.Students;

public class StudentService(
    IUserService userService,
    IStudentRepository studentRepository,
    IGroupRepository groupRepository) : IStudentService
{
    public async Task<ErrorOr<CreateStudentResponse>> CreateAsync(CreateStudentRequest request, Guid groupId)
    {
        var group = await groupRepository.GetByIdAsync(groupId);
        if (group == null) return Error.NotFound("Group not found");
        
        var userResult = await userService.CreateAsync(
            new CreateUserRequest(request.FullName, request.Gender));

        if (userResult.IsError)
            return userResult.FirstError;

        var user = userResult.Value;
        
        var firstParentInfo = new ParentInfo(request.FirstParentInfo.Name, request.FirstParentInfo.Contact);
        
        var secondParentInfo = request.SecondParentInfo == null ? null
            : new ParentInfo(request.SecondParentInfo.Name, request.SecondParentInfo.Contact);
        
        var student = new Student(
            user.Id, 
            request.BirthDate.ToUniversalTime(), 
            request.SchoolGrade, 
            request.Kyu,
            request.Address, 
            firstParentInfo, 
            secondParentInfo);
        
        student.ChangeGroup(groupId);
        
        studentRepository.AddStudent(student);
        await studentRepository.SaveChangesAsync();

        return new CreateStudentResponse(student.Id, user.Username, user.Password, student.User.FullName.ToString());
    }

    public async Task<ErrorOr<List<StudentItemDto>>> GetStudentsByGroupAsync(Guid groupId, Guid userId)
    {
        var group = await groupRepository.GetByIdAsync(groupId);
        if (group == null) return Error.NotFound(description: "Group not found");

        if (group.TrainerId != userId)
        {
            var student = await studentRepository.GetByUserIdAsync(userId);
            if (student == null || student.GroupId == groupId) 
                return Error.Forbidden(description: "You don't have access to this group");
        }
        
        var students = await studentRepository.GetAllByGroupIdAsync(groupId);
        return students.Select(s => s.ToItemDto()).ToList();
    }
}