using TrainerJournal.Application.Services.Practices.Dtos;
using TrainerJournal.Application.Services.Practices.Dtos.Requests;

namespace TrainerJournal.Application.Services.Practices;

public class PracticeService : IPracticeService
{
    public Task<PracticeDto> GetPractice(Guid userId, Guid practiceId, DateTime practiceStart)
    {
        throw new NotImplementedException();
    }

    public Task<PracticeDto> CreateSinglePracticeAsync(Guid trainerId, CreateSinglePracticeRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<PracticeDto> ChangePracticeAsync(Guid trainerId, ChangePracticeRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<PracticeDto> CancelPracticeAsync(Guid trainerId, Guid practiceId, DateTime practiceStart)
    {
        throw new NotImplementedException();
    }
}