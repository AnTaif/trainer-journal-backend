using TrainerJournal.Application.Services.Practices.Dtos;
using TrainerJournal.Application.Services.Practices.Dtos.Requests;

namespace TrainerJournal.Application.Services.Practices;

public interface IPracticeService
{
    public Task<PracticeDto> GetPractice(Guid userId, Guid practiceId, DateTime practiceStart);
    
    public Task<PracticeDto> CreateSinglePracticeAsync(Guid trainerId, CreateSinglePracticeRequest request);

    public Task<PracticeDto> ChangePracticeAsync(Guid trainerId, ChangePracticeRequest request);

    public Task<PracticeDto> CancelPracticeAsync(Guid trainerId, Guid practiceId, DateTime practiceStart);
}