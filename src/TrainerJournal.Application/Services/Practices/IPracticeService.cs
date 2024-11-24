using TrainerJournal.Application.Services.Practices.Dtos;
using TrainerJournal.Application.Services.Practices.Dtos.Requests;
using TrainerJournal.Domain.Common;

namespace TrainerJournal.Application.Services.Practices;

public interface IPracticeService
{
    public Task<Result<PracticeDto>> GetPractice(Guid userId, Guid practiceId, DateTime practiceDate);

    public Task<Result<PracticeDto>> CreateSinglePracticeAsync(Guid trainerId, CreateSinglePracticeRequest request);

    public Task<Result<PracticeDto>>
        ChangePracticeAsync(Guid trainerId, Guid practiceId, ChangePracticeRequest request);

    public Task<Result<PracticeDto>>
        CancelPracticeAsync(Guid trainerId, Guid practiceId, CancelPracticeRequest request);

    public Task<Result<PracticeDto>> ResumePracticeAsync(Guid trainerId, Guid id);
}