using ErrorOr;
using TrainerJournal.Application.Services.Groups;
using TrainerJournal.Application.Services.Practices.Dtos;
using TrainerJournal.Application.Services.Practices.Dtos.Requests;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Enums.PracticeType;

namespace TrainerJournal.Application.Services.Practices;

public class PracticeService(
    IGroupRepository groupRepository,
    IPracticeManager practiceManager,
    IPracticeRepository practiceRepository) : IPracticeService
{
    public async Task<ErrorOr<PracticeDto>> GetPractice(Guid userId, Guid practiceId, DateTime practiceDate)
    {
        var practiceResult = await practiceManager.GetBasePracticeAsync(practiceId, practiceDate);
        if (practiceResult.IsError) return practiceResult.FirstError;

        await practiceRepository.SaveChangesAsync();
        return practiceResult.Value.ToDto(practiceDate);
    }

    public async Task<ErrorOr<PracticeDto>> CreateSinglePracticeAsync(Guid trainerId, CreateSinglePracticeRequest request)
    {
        var group = await groupRepository.GetByIdAsync(request.GroupId);
        if (group == null) return Error.NotFound(description: "Group not found");

        var newPractice = new SinglePractice(request.GroupId, request.Price ?? group.Price, request.Start, request.End,
                                             request.PracticeType.ToPracticeTypeEnum(), trainerId);

        await practiceRepository.AddAsync(newPractice);
        await practiceRepository.SaveChangesAsync();

        return await GetSinglePracticeDtoAsync(newPractice.Id);
    }

    public async Task<ErrorOr<PracticeDto>> ChangePracticeAsync(Guid trainerId, Guid practiceId, ChangePracticeRequest request)
    {
        var practiceResult = await practiceManager.UpdateSpecificPracticeAsync(
            practiceId, 
            request.PracticeStart, 
            request.GroupId, 
            request.NewStart, 
            request.NewEnd, 
            request.PracticeType, 
            request.Price);
        if (practiceResult.IsError) return practiceResult.FirstError;
        
        await practiceRepository.SaveChangesAsync();
        return practiceResult.Value.ToDto(request.PracticeStart);
    }

    public async Task<ErrorOr<PracticeDto>> CancelPracticeAsync(Guid trainerId, Guid practiceId, CancelPracticeRequest request)
    {
        var practiceResult = await practiceManager.CancelSpecificPracticeAsync(
            practiceId, 
            request.PracticeStart, 
            request.Comment);
        if (practiceResult.IsError) return practiceResult.FirstError;
        
        await practiceRepository.SaveChangesAsync();
        return practiceResult.Value.ToDto(request.PracticeStart);
    }

    private async Task<ErrorOr<PracticeDto>> GetSinglePracticeDtoAsync(Guid practiceId)
    {
        return await practiceRepository.GetByIdAsync(practiceId) is SinglePractice practice 
            ? practice.ToDto() : throw new Exception("Practice was not created after SaveChangesAsync().");
    }
}