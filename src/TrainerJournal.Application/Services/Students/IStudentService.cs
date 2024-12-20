using TrainerJournal.Application.Services.Students.Dtos;
using TrainerJournal.Application.Services.Students.Dtos.Requests;
using TrainerJournal.Application.Services.Students.Dtos.Responses;
using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.Students;

public interface IStudentService
{
    public Task<Result<List<StudentItemDto>>> GetStudentsByTrainerAsync(Guid trainerId, bool withGroup);

    public Task<Result<CreateStudentResponse>> CreateAsync(CreateStudentRequest request);

    public Task<Result<List<StudentItemDto>>> GetStudentsByGroupAsync(Guid groupId, Guid userId);

    public Task<Result<StudentInfoDto>> AddStudentToGroupAsync(Guid groupId, AddStudentRequest request, Guid trainerId);

    public Task<Result> ExcludeStudentFromGroupAsync(Guid groupId, string studentUsername,
        Guid trainerId);
}