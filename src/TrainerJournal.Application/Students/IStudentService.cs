using TrainerJournal.Application.Students.Requests;
using TrainerJournal.Application.Students.Responses;

namespace TrainerJournal.Application.Students;

public interface IStudentService
{
    public Task<CreateStudentResponse> CreateAsync(CreateStudentRequest request, Guid groupId);
}