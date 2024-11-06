using ErrorOr;
using TrainerJournal.Application.Services.Groups;
using TrainerJournal.Application.Services.Practices.Dtos;
using TrainerJournal.Application.Services.Practices.Dtos.Requests;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Enums.PracticeType;

namespace TrainerJournal.Application.Services.Practices;

public class PracticeService(
    IGroupRepository groupRepository,
    IPracticeRepository practiceRepository) : IPracticeService
{
    public async Task<ErrorOr<PracticeDto>> GetPractice(Guid userId, Guid practiceId, DateTime practiceDate)
    {
        var practice = await practiceRepository.GetByIdAsync(practiceId);
        if (practice == null) return Error.NotFound(description: "Practice not found");
        
        if (practice is SinglePractice singlePractice)
        {
            return singlePractice.ToDto();
        }

        if (practice is SchedulePractice schedulePractice)
        {
            if (practice.Start.DayOfWeek != practiceDate.DayOfWeek)
                return Error.Validation(description: "Original practice and the 'practiceDate' must be the same day of the week.");
            
            return schedulePractice.ToDto(practiceDate);
        }
        
        throw new Exception("Practice haven't type???");
    }

    public async Task<ErrorOr<PracticeDto>> CreateSinglePracticeAsync(Guid trainerId, CreateSinglePracticeRequest request)
    {
        var group = await groupRepository.GetByIdAsync(request.GroupId);
        if (group == null) return Error.NotFound(description: "Group not found");
        
        var newPractice = new SinglePractice(request.GroupId, request.Price ?? group.Price, request.Start, request.End,
            request.PracticeType.ToPracticeTypeEnum(), trainerId);

        await practiceRepository.AddAsync(newPractice);
        await practiceRepository.SaveChangesAsync();

        var practice = await practiceRepository.GetByIdAsync(newPractice.Id) as SinglePractice;
        if (practice == null) return Error.Failure(description: "Practice does not created");
        
        return practice.ToDto();
    }

    public async Task<ErrorOr<PracticeDto>> ChangePracticeAsync(Guid trainerId, Guid practiceId, ChangePracticeRequest request)
    {
        var practice = await practiceRepository.GetByIdAsync(practiceId);
        if (practice == null) return Error.NotFound(description: "Practice not found");
        
        if (practice is SinglePractice singlePractice)
        {
            singlePractice.Update(request.GroupId, request.NewStart, request.NewEnd,
                request.PracticeType?.ToPracticeTypeEnum(), request.Price);
            await practiceRepository.SaveChangesAsync();
            
            return singlePractice.ToDto();
        }

        if (practice is SchedulePractice schedulePractice)
        {
            if (practice.Start.DayOfWeek != request.currentStart.DayOfWeek)
                return Error.Validation(metadata: new Dictionary<string, object>
                {
                    {"practiceStart", "Original practice and the 'currentStart' must be the same day of the week."}
                });

            var newSinglePractice = new SinglePractice(
                request.GroupId ?? schedulePractice.GroupId,
                request.Price ?? schedulePractice.Price,
                request.NewStart ?? SchedulePractice.CombineDateAndTime(request.currentStart, schedulePractice.Start),
                request.NewEnd ?? SchedulePractice.CombineDateAndTime(request.currentStart, schedulePractice.End),
                request.PracticeType?.ToPracticeTypeEnum() ?? schedulePractice.PracticeType, trainerId,
                schedulePractice.Id, 
                SchedulePractice.CombineDateAndTime(request.currentStart, schedulePractice.Start));

            await practiceRepository.AddAsync(newSinglePractice);
            await practiceRepository.SaveChangesAsync();

            var newPractice = await practiceRepository.GetByIdAsync(newSinglePractice.Id) as SinglePractice;
            if (newPractice == null) return Error.Failure(description: "Practice does not created");
            
            return newPractice.ToDto();
        }
        
        throw new Exception("Practice haven't type???");
    }

    public async Task<ErrorOr<PracticeDto>> CancelPracticeAsync(Guid trainerId, Guid practiceId, CancelPracticeRequest request)
    {
        var practice = await practiceRepository.GetByIdAsync(practiceId);
        if (practice == null) return Error.NotFound(description: "Practice not found");
        
        if (practice is SinglePractice singlePractice)
        {
            singlePractice.Cancel(request.Comment);
            await practiceRepository.SaveChangesAsync();
            
            return singlePractice.ToDto();
        }

        if (practice is SchedulePractice schedulePractice)
        {
            if (practice.Start.DayOfWeek != request.PracticeStart.DayOfWeek)
                return Error.Validation(metadata: new Dictionary<string, object>
                {
                    {"practiceStart", "Original practice and the 'currentStart' must be the same day of the week."}
                });

            var newSinglePractice = new SinglePractice(
                schedulePractice.GroupId,
                schedulePractice.Price,
                SchedulePractice.CombineDateAndTime(request.PracticeStart, schedulePractice.Start),
                SchedulePractice.CombineDateAndTime(request.PracticeStart, schedulePractice.End),
                schedulePractice.PracticeType, 
                trainerId,
                schedulePractice.Id, 
                SchedulePractice.CombineDateAndTime(request.PracticeStart, schedulePractice.Start));
            
            newSinglePractice.Cancel(request.Comment);

            await practiceRepository.AddAsync(newSinglePractice);
            await practiceRepository.SaveChangesAsync();

            var newPractice = await practiceRepository.GetByIdAsync(newSinglePractice.Id) as SinglePractice;
            if (newPractice == null) return Error.Failure(description: "Practice does not created");
            
            return newPractice.ToDto();
        }
        
        throw new Exception("Practice haven't type???");
    }
}