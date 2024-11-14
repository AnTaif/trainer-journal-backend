using ErrorOr;
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
    IGroupRepository groupRepository) : IStudentService
{
    public async Task<ErrorOr<List<StudentItemDto>>> GetStudentsByTrainerAsync(Guid trainerId, bool withGroup)
    {
        var students = await studentRepository.GetAllByTrainerIdAsync(trainerId, withGroup);

        return students.Select(s => s.ToItemDto()).ToList();
    }

    public async Task<ErrorOr<CreateStudentResponse>> CreateAsync(CreateStudentRequest request, Guid? groupId)
    {
        Group? group = null;
        if (groupId != null)
        {
            group = await groupRepository.GetByIdAsync(groupId.Value);
            if (group == null) return Error.NotFound("Group not found");
        }
        
        var userResult = await userService.CreateAsync(
            new CreateUserRequest(request.FullName, request.Gender));

        if (userResult.IsError)
            return userResult.FirstError;

        var user = userResult.Value;
        
        var student = new Student(
            user.Id, 
            request.BirthDate.ToUniversalTime(), 
            request.SchoolGrade, 
            request.Kyu,
            request.Address, 
            request.Contacts.Select(c => c.ToEntity()).ToList());

        if (groupId != null)
        {
            student.AddToGroup(group!);
        }
        
        studentRepository.AddStudent(student);
        await studentRepository.SaveChangesAsync();

        return new CreateStudentResponse(student.UserId, user.Username, user.Password, student.User.FullName.ToString());
    }

    public async Task<ErrorOr<List<StudentItemDto>>> GetStudentsByGroupAsync(Guid groupId, Guid userId)
    {
        var group = await groupRepository.GetByIdAsync(groupId);
        if (group == null) return Error.NotFound("Group not found");
        
        var students = await studentRepository.GetAllByGroupIdAsync(groupId);
        return students.Select(s => s.ToItemDto()).ToList();
    }

    public async Task<ErrorOr<StudentInfoDto>> AddStudentToGroupAsync(Guid groupId, Guid studentId, Guid trainerId)
    {
        var student = await studentRepository.GetByUserIdAsync(studentId);
        if (student == null) return Error.NotFound("Student not found");

        var group = await groupRepository.GetByIdAsync(groupId);
        if (group == null) return Error.NotFound("Group not found");

        student.AddToGroup(group);
        await studentRepository.SaveChangesAsync();
        
        return student.ToInfoDto();
    }

    public async Task<ErrorOr<StudentInfoDto>> ExcludeStudentFromGroupAsync(Guid groupId, Guid studentId, Guid trainerId)
    {
        var student = await studentRepository.GetByUserIdAsync(studentId);
        if (student == null) return Error.NotFound("Student not found");

        var group = await groupRepository.GetByIdAsync(groupId);
        if (group == null) return Error.NotFound("Group not found");

        student.ExcludeFromGroup(group);
        await studentRepository.SaveChangesAsync();
        
        return student.ToInfoDto();
    }
}