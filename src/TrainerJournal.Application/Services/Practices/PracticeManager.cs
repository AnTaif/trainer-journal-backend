using ErrorOr;
using Microsoft.Extensions.Logging;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Enums.PracticeType;

namespace TrainerJournal.Application.Services.Practices;

public class PracticeManager(
    ILogger<PracticeManager> logger,
    IPracticeRepository practiceRepository) : IPracticeManager
{
    public async Task<ErrorOr<Practice>> GetBasePracticeAsync(Guid id, DateTime time)
    {
        var practice = await practiceRepository.GetByIdAsync(id);
        if (practice == null) return Error.NotFound(description: "Group not found");

        if (practice is SinglePractice singlePractice)
        {
            return singlePractice;
        }

        if (practice is SchedulePractice schedulePractice)
        {
            if (!IsPracticeDateValid(schedulePractice, time))
                return Error.Validation(
                    description: "Original SchedulePractice and the 'practiceDate' must be the same day of the week");
            if (await IsSchedulePracticeOverridenAsync(schedulePractice.Id, time))
                return Error.NotFound(
                    description: "This SchedulePractice is overriden by different single practice");

            return schedulePractice;
        }

        throw new Exception("Practice type is unrecognized");
    }

    public async Task<ErrorOr<Practice>> UpdateSpecificPracticeAsync(Guid practiceId, DateTime practiceStart, Guid? groupId, 
        DateTime? newStart, DateTime? newEnd, string? practiceType, float? price)
    {
        var practiceResult = await GetBasePracticeAsync(practiceId, practiceStart);
        if (practiceResult.IsError) return practiceResult;
        var practice = practiceResult.Value;

        if (practice is SinglePractice singlePractice)
        {
            singlePractice.Update(groupId, newStart, newEnd, practiceType?.ToPracticeTypeEnum(), price);
            return singlePractice;
        }

        if (practice is SchedulePractice schedulePractice)
        {
            return await CreatePracticeFromScheduleAsync(
            schedulePractice, practiceStart, newStart, newEnd, price, practiceType?.ToPracticeTypeEnum());
        }

        throw new Exception("Practice type is unrecognized");
    }

    public async Task<ErrorOr<Practice>> CancelSpecificPracticeAsync(
        Guid practiceId, DateTime practiceStart, string comment = "")
    {
        var practiceResult = await GetBasePracticeAsync(practiceId, practiceStart);
        if (practiceResult.IsError) return practiceResult;
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
        
        throw new Exception("Practice type is unrecognized");
    }

    private async Task<SinglePractice> CreatePracticeFromScheduleAsync(
        SchedulePractice schedulePractice,
        DateTime currentStart, 
        DateTime? newStart = null, 
        DateTime? newEnd = null, 
        float? price = null,
        PracticeType? practiceType = null)
    {
        var newPractice = new SinglePractice(
            schedulePractice.GroupId,
            price ?? schedulePractice.Price,
            newStart ?? SchedulePractice.CombineDateAndTime(currentStart, schedulePractice.Start),
            newEnd ?? SchedulePractice.CombineDateAndTime(currentStart, schedulePractice.End),
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
        logger.LogInformation("SchedulePractice is overriden");
        return await practiceRepository.HasOverridenSinglePracticeAsync(practiceId, practiceStart);
    }
}