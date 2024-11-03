using ErrorOr;
using TrainerJournal.Application.Services.Students.Dtos;
using TrainerJournal.Application.Services.Students.Dtos.Requests;
using TrainerJournal.Application.Services.Students.Dtos.Responses;

namespace TrainerJournal.Application.Services.Students;

public interface IStudentService
{
    public Task<ErrorOr<CreateStudentResponse>> CreateAsync(CreateStudentRequest request, Guid groupId);

    public Task<ErrorOr<List<StudentItemDto>>> GetStudentsByGroupAsync(Guid groupId, Guid userId);
    
    public Task<ErrorOr<StudentInfoDto>> ChangeStudentGroupAsync(Guid studentId, Guid trainerId, ChangeStudentGroupRequest request);
}