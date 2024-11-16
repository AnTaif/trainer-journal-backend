using ErrorOr;
using TrainerJournal.Application.Services.Practices.Dtos;
using TrainerJournal.Application.Services.Practices.Dtos.Requests;

namespace TrainerJournal.Application.Services.Practices;

public interface IPracticeService
{
    public Task<ErrorOr<PracticeDto>> GetPractice(Guid userId, Guid practiceId, DateTime practiceDate);
    
    public Task<ErrorOr<PracticeDto>> CreateSinglePracticeAsync(Guid trainerId, CreateSinglePracticeRequest request);

    public Task<ErrorOr<PracticeDto>> ChangePracticeAsync(Guid trainerId, Guid practiceId, ChangePracticeRequest request);

    public Task<ErrorOr<PracticeDto>> CancelPracticeAsync(Guid trainerId, Guid practiceId, CancelPracticeRequest request);

    public Task<ErrorOr<PracticeDto>> ActivatePracticeAsync(Guid trainerId, Guid id);
}