using ErrorOr;
using TrainerJournal.Application.Students.Dtos.Requests;
using TrainerJournal.Application.Students.Dtos.Responses;

namespace TrainerJournal.Application.Students;

public interface IStudentService
{
    public Task<ErrorOr<CreateStudentResponse>> CreateAsync(CreateStudentRequest request, Guid groupId);
}