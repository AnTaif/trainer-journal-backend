using TrainerJournal.Application.Services.Groups;
using TrainerJournal.Application.Services.Practices.Dtos;
using TrainerJournal.Application.Services.Practices.Dtos.Requests;
using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Enums.PracticeType;

namespace TrainerJournal.Application.Services.Practices;

public class PracticeService(
    IGroupRepository groupRepository,
    IPracticeManager practiceManager,
    IPracticeRepository practiceRepository) : IPracticeService
{
    public async Task<Result<PracticeDto>> GetPractice(Guid userId, Guid practiceId, DateTime practiceDate)
    {
        var practiceResult = await practiceManager.GetBasePracticeAsync(practiceId, practiceDate);
        if (practiceResult.IsError()) return practiceResult.Error;

        await practiceRepository.SaveChangesAsync();
        return practiceResult.Value.ToDto(practiceDate);
    }

    public async Task<Result<PracticeDto>> CreateSinglePracticeAsync(Guid trainerId,
        CreateSinglePracticeRequest request)
    {
        var group = await groupRepository.GetByIdAsync(request.GroupId);
        if (group == null) return Error.NotFound("Group not found");

        var newPractice = new SinglePractice(request.GroupId, request.Price ?? group.Price, request.Start, request.End,
            request.HallAddress ?? group.HallAddress, request.PracticeType.ToPracticeTypeEnum(), trainerId);

        await practiceRepository.AddAsync(newPractice);
        await practiceRepository.SaveChangesAsync();

        return await GetSinglePracticeDtoAsync(newPractice.Id);
    }

    public async Task<Result<PracticeDto>> ChangePracticeAsync(Guid trainerId, Guid practiceId,
        ChangePracticeRequest request)
    {
        var practiceResult = await practiceManager.UpdateSpecificPracticeAsync(
            practiceId,
            request.PracticeStart,
            request.GroupId,
            request.NewStart,
            request.NewEnd,
            request.HallAddress,
            request.PracticeType,
            request.Price);
        if (practiceResult.IsError()) return practiceResult.Error;

        await practiceRepository.SaveChangesAsync();
        return practiceResult.Value.ToDto(request.PracticeStart);
    }

    public async Task<Result<PracticeDto>> CancelPracticeAsync(Guid trainerId, Guid practiceId,
        CancelPracticeRequest request)
    {
        var practiceResult = await practiceManager.CancelSpecificPracticeAsync(
            practiceId,
            request.PracticeStart,
            request.Comment ?? "");
        if (practiceResult.IsError()) return practiceResult.Error;

        await practiceRepository.SaveChangesAsync();
        return practiceResult.Value.ToDto(request.PracticeStart);
    }

    public async Task<Result<PracticeDto>> ActivatePracticeAsync(Guid trainerId, Guid id)
    {
        var practiceResult = await practiceManager.GetBasePracticeAsync(id, DateTime.MinValue);
        if (practiceResult.IsError()) return practiceResult.Error;

        if (practiceResult.Value is not SinglePractice singlePractice)
            return Error.BadRequest("Practice has never changed");

        if (!singlePractice.IsCanceled)
            return Error.BadRequest("Practice is already active");

        if (singlePractice.IsIdenticalToOverridenPractice())
        {
            practiceRepository.Remove(singlePractice);
            await practiceRepository.SaveChangesAsync();
            return singlePractice.OverridenPractice!.ToDto(singlePractice.OriginalStart!.Value);
        }

        singlePractice.Activate();
        await practiceRepository.SaveChangesAsync();
        return singlePractice.ToDto(DateTime.MinValue);
    }

    private async Task<Result<PracticeDto>> GetSinglePracticeDtoAsync(Guid practiceId)
    {
        return await practiceRepository.GetByIdAsync(practiceId) is SinglePractice practice
            ? practice.ToDto()
            : throw new Exception("Practice was not created after SaveChangesAsync().");
    }
}