using Microsoft.Extensions.Logging;
using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Enums.PracticeType;

namespace TrainerJournal.Application.Services.Practices;

public class PracticeManager(
    ILogger<PracticeManager> logger,
    IPracticeRepository practiceRepository) : IPracticeManager
{
    public async Task<Result<Practice>> GetBasePracticeAsync(Guid id, DateTime time)
    {
        var practice = await practiceRepository.GetByIdAsync(id);
        return await InnerGetBasePracticeAsync(practice, time);
    }
    
    public async Task<Result<Practice>> GetBasePracticeWithIncludesAsync(Guid id, DateTime time)
    {
        var practice = await practiceRepository.GetByIdWithIncludesAsync(id);
        return await InnerGetBasePracticeAsync(practice, time);
    }

    private async Task<Result<Practice>> InnerGetBasePracticeAsync(Practice? practice, DateTime time)
    {
        if (practice == null) return Error.NotFound("Group not found");

        if (practice is SinglePractice singlePractice)
        {
            return singlePractice;
        }

        if (practice is SchedulePractice schedulePractice)
        {
            if (!IsPracticeDateValid(schedulePractice, time))
                return Error.BadRequest("Original SchedulePractice and the 'practiceDate' must be the same day of the week");
            if (await IsSchedulePracticeOverridenAsync(schedulePractice.Id, time))
                return Error.NotFound("This SchedulePractice is overriden by different single practice");

            return schedulePractice;
        }

        throw new Exception("Practice type is unrecognized");
    }

    public async Task<Result<Practice>> UpdateSpecificPracticeAsync(Guid practiceId, DateTime practiceStart, Guid? groupId, 
        DateTime? newStart, DateTime? newEnd, string? hallAddress, string? practiceType, float? price)
    {
        var practiceResult = await GetBasePracticeWithIncludesAsync(practiceId, practiceStart);
        if (practiceResult.IsError()) return practiceResult.Error;
        var practice = practiceResult.Value;

        if (practice is SinglePractice singlePractice)
        {
            singlePractice.Update(groupId, newStart, newEnd, practiceType?.ToPracticeTypeEnum(), price);
            return singlePractice;
        }

        if (practice is SchedulePractice schedulePractice)
        {
            return await CreatePracticeFromScheduleAsync(
            schedulePractice, practiceStart, newStart, newEnd, price, hallAddress, practiceType?.ToPracticeTypeEnum());
        }

        throw new Exception("Practice type is unrecognized");
    }

    public async Task<Result<Practice>> CancelSpecificPracticeAsync(
        Guid practiceId, DateTime practiceStart, string comment = "")
    {
        var practiceResult = await GetBasePracticeWithIncludesAsync(practiceId, practiceStart);
        if (practiceResult.IsError()) return practiceResult.Error;
        var practice = practiceResult.Value;

        if (practice is SinglePractice singlePractice)
        {
            singlePractice.Cancel(comment);
            return singlePractice;
        }

        if (practice is SchedulePractice schedulePractice)
        {
            var newPractice = await CreatePracticeFromScheduleAsync(schedulePractice, practiceStart);
            newPractice.Cancel(comment);
            return newPractice;
        } 
        
        logger.LogError("CancelPractice: {id} - {time} Practice type is unrecognized", practiceId, practiceStart);
        throw new Exception("Practice type is unrecognized");
    }

    private async Task<SinglePractice> CreatePracticeFromScheduleAsync(
        SchedulePractice schedulePractice,
        DateTime currentStart, 
        DateTime? newStart = null, 
        DateTime? newEnd = null, 
        float? price = null,
        string? hallAddress = null,
        PracticeType? practiceType = null)
    {
        var newPractice = new SinglePractice(
            schedulePractice.GroupId,
            price ?? schedulePractice.Price,
            newStart ?? SchedulePractice.CombineDateAndTime(currentStart, schedulePractice.Start),
            newEnd ?? SchedulePractice.CombineDateAndTime(currentStart, schedulePractice.End),
            hallAddress ?? schedulePractice.HallAddress,
            practiceType ?? schedulePractice.PracticeType,
            schedulePractice.TrainerId,
            schedulePractice.Id,
            SchedulePractice.CombineDateAndTime(currentStart, schedulePractice.Start)
        );
        await practiceRepository.AddAsync(newPractice);
        return newPractice;
    }
    
    private bool IsPracticeDateValid(SchedulePractice schedulePractice, DateTime practiceDate)
        => schedulePractice.Start.DayOfWeek == practiceDate.DayOfWeek 
           && schedulePractice.Start.TimeOfDay == practiceDate.TimeOfDay;

    private async Task<bool> IsSchedulePracticeOverridenAsync(Guid practiceId, DateTime practiceStart)
    {
        return await practiceRepository.HasOverridenSinglePracticeAsync(practiceId, practiceStart);
    }
}