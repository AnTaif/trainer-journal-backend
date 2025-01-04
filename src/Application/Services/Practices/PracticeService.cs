using TrainerJournal.Application.Services.Groups;
using TrainerJournal.Application.Services.Practices.Dtos;
using TrainerJournal.Application.Services.Practices.Dtos.Requests;
using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Common.Result;
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
        var practiceResult = await practiceManager.GetBasePracticeWithIncludesAsync(practiceId, practiceDate);
        if (practiceResult.IsError()) return practiceResult.Error;

        await practiceRepository.SaveChangesAsync();
        return practiceResult.Value.ToDto(practiceDate);
    }

    public async Task<Result<PracticeDto>> CreateSinglePracticeAsync(Guid trainerId,
        CreateSinglePracticeRequest request)
    {
        var hallAddress = request.HallAddress ?? "";
        var price = request.Price;
        
        if (request.GroupId != null)
        {
            var group = await groupRepository.FindByIdAsync(request.GroupId.Value);
            if (group == null) return Error.NotFound("Group not found");

            hallAddress = group.HallAddress;
            price = group.Price;
        }

        var newPractice = new SinglePractice(request.GroupId, price!.Value, request.Start, request.End,
            hallAddress, request.PracticeType.ToPracticeTypeEnum(), trainerId);

        practiceRepository.Add(newPractice);
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

    public async Task<Result<PracticeDto>> ResumePracticeAsync(Guid trainerId, Guid id)
    {
        var practiceResult = await practiceManager.GetBasePracticeWithIncludesAsync(id, DateTime.MinValue);
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
        return await practiceRepository.FindByIdWithIncludesAsync(practiceId) is SinglePractice practice
            ? practice.ToDto()
            : throw new Exception("Practice was not created after SaveChangesAsync().");
    }
}