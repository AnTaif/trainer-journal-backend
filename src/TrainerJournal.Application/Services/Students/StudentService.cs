using TrainerJournal.Application.Services.Groups;
using TrainerJournal.Application.Services.Students.Dtos;
using TrainerJournal.Application.Services.Students.Dtos.Requests;
using TrainerJournal.Application.Services.Students.Dtos.Responses;
using TrainerJournal.Application.Services.Users;
using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Enums.Gender;

namespace TrainerJournal.Application.Services.Students;

public class StudentService(
    IUserService userService,
    IStudentRepository studentRepository,
    IGroupRepository groupRepository) : IStudentService
{
    public async Task<Result<List<StudentItemDto>>> GetStudentsByTrainerAsync(Guid trainerId, bool withGroup)
    {
        var students = await studentRepository.GetAllByTrainerIdAsync(trainerId, withGroup);

        return students.Select(s => s.ToItemDto()).ToList();
    }

    public async Task<Result<CreateStudentResponse>> CreateAsync(CreateStudentRequest request)
    {
        var groups = new List<Group>();
        if (request.GroupIds.Count > 0)
            foreach (var groupId in request.GroupIds)
            {
                var group = await groupRepository.GetByIdAsync(groupId);
                if (group == null) return Error.NotFound("Group not found");
                groups.Add(group);
            }

        var userResult = await userService.CreateAsync(request.FullName, request.Gender.ToGenderEnum());
        if (userResult.IsError()) return userResult.Error;
        var user = userResult.Value;

        var student = new Student(
            user.Id,
            request.BirthDate.ToUniversalTime(),
            request.SchoolGrade,
            request.Kyu,
            request.Address,
            request.Contacts.Select(c => c.ToEntity()).ToList());

        if (request.GroupIds.Count > 0)
            foreach (var group in groups)
                student.AddToGroup(group);

        studentRepository.AddStudent(student);
        await studentRepository.SaveChangesAsync();

        return new CreateStudentResponse
        {
            Username = user.UserName!,
            Password = user.Password,
            FullName = user.FullName.ToString()
        };
    }

    public async Task<Result<List<StudentItemDto>>> GetStudentsByGroupAsync(Guid groupId, Guid userId)
    {
        var group = await groupRepository.GetByIdAsync(groupId);
        if (group == null) return Error.NotFound("Group not found");

        var students = await studentRepository.GetAllByGroupIdAsync(groupId);
        return students.Select(s => s.ToItemDto()).ToList();
    }

    public async Task<Result<StudentInfoDto>> AddStudentToGroupAsync(Guid groupId, AddStudentRequest request,
        Guid trainerId)
    {
        var student = await studentRepository.GetByUsernameWithIncludesAsync(request.StudentUsername);
        if (student == null) return Error.NotFound("Student not found");

        var group = await groupRepository.GetByIdAsync(groupId);
        if (group == null) return Error.NotFound("Group not found");

        student.AddToGroup(group);
        await studentRepository.SaveChangesAsync();

        return student.ToInfoDto();
    }

    public async Task<Result> ExcludeStudentFromGroupAsync(
        Guid groupId, string studentUsername, Guid trainerId)
    {
        var student = await studentRepository.GetByUsernameWithIncludesAsync(studentUsername);
        if (student == null) return Error.NotFound("Student not found");

        var group = await groupRepository.GetByIdAsync(groupId);
        if (group == null) return Error.NotFound("Group not found");

        student.ExcludeFromGroup(group);
        await studentRepository.SaveChangesAsync();

        return Result.Success();
    }
}