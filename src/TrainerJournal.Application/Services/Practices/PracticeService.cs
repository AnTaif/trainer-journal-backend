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
        if (practice == null) return Error.NotFound("Practice not found");
        
        return practice switch
        {
            SinglePractice singlePractice => singlePractice.ToDto(),
            SchedulePractice schedulePractice => IsPracticeDateValid(schedulePractice, practiceDate) 
                ? schedulePractice.ToDto(practiceDate) 
                : Error.Validation("Original practice and the 'practiceDate' must be the same day of the week"),
            _ => throw new Exception("Practice type is unrecognized")
        };
    }

    public async Task<ErrorOr<PracticeDto>> CreateSinglePracticeAsync(Guid trainerId, CreateSinglePracticeRequest request)
    {
        var group = await groupRepository.GetByIdAsync(request.GroupId);
        if (group == null) return Error.NotFound("Group not found");

        var newPractice = new SinglePractice(request.GroupId, request.Price ?? group.Price, request.Start, request.End,
                                             request.PracticeType.ToPracticeTypeEnum(), trainerId);

        await practiceRepository.AddAsync(newPractice);
        await practiceRepository.SaveChangesAsync();

        return await GetPracticeDtoAsync(newPractice.Id);
    }

    public async Task<ErrorOr<PracticeDto>> ChangePracticeAsync(Guid trainerId, Guid practiceId, ChangePracticeRequest request)
    {
        var practice = await practiceRepository.GetByIdAsync(practiceId);
        if (practice == null) return Error.NotFound("Practice not found");
        
        return practice switch
        {
            SinglePractice singlePractice => await UpdateSinglePractice(singlePractice, request),
            SchedulePractice schedulePractice => await ChangeSchedulePractice(schedulePractice, trainerId, request),
            _ => throw new Exception("Practice type is unrecognized")
        };
    }

    public async Task<ErrorOr<PracticeDto>> CancelPracticeAsync(Guid trainerId, Guid practiceId, CancelPracticeRequest request)
    {
        var practice = await practiceRepository.GetByIdAsync(practiceId);
        if (practice == null) return Error.NotFound("Practice not found");
        
        return practice switch
        {
            SinglePractice singlePractice => await CancelSinglePractice(singlePractice, request),
            SchedulePractice schedulePractice => await CancelSchedulePractice(schedulePractice, trainerId, request),
            _ => throw new Exception("Practice type is unrecognized")
        };
    }

    private async Task<ErrorOr<PracticeDto>> UpdateSinglePractice(
        SinglePractice singlePractice, 
        ChangePracticeRequest request)
    {
        singlePractice.Update(request.GroupId, request.NewStart, request.NewEnd, request.PracticeType?.ToPracticeTypeEnum(), request.Price);
        await practiceRepository.SaveChangesAsync();
        return singlePractice.ToDto();
    }

    private async Task<ErrorOr<PracticeDto>> ChangeSchedulePractice(
        SchedulePractice schedulePractice, 
        Guid trainerId, 
        ChangePracticeRequest request)
    {
        if (!IsPracticeDateValid(schedulePractice, request.CurrentStart))
            return Error.Validation("Original practice and the 'currentStart' must be the same day of the week");

        var newPractice = CreateSinglePracticeFromSchedule(schedulePractice, trainerId, request.CurrentStart, request.NewStart, request.NewEnd, request.Price, request.PracticeType?.ToPracticeTypeEnum());
        await practiceRepository.AddAsync(newPractice);
        await practiceRepository.SaveChangesAsync();

        return await GetPracticeDtoAsync(newPractice.Id);
    }

    private async Task<ErrorOr<PracticeDto>> CancelSinglePractice(
        SinglePractice singlePractice, 
        CancelPracticeRequest request)
    {
        singlePractice.Cancel(request.Comment);
        await practiceRepository.SaveChangesAsync();
        return singlePractice.ToDto();
    }

    private async Task<ErrorOr<PracticeDto>> CancelSchedulePractice(
        SchedulePractice schedulePractice, 
        Guid trainerId, 
        CancelPracticeRequest request)
    {
        if (!IsPracticeDateValid(schedulePractice, request.PracticeStart))
            return Error.Validation("Original practice and the 'currentStart' must be the same day of the week");

        var newPractice = CreateSinglePracticeFromSchedule(schedulePractice, trainerId, request.PracticeStart);
        newPractice.Cancel(request.Comment);
        
        await practiceRepository.AddAsync(newPractice);
        await practiceRepository.SaveChangesAsync();

        return await GetPracticeDtoAsync(newPractice.Id);
    }

    private static bool IsPracticeDateValid(SchedulePractice schedulePractice, DateTime practiceDate)
        => schedulePractice.Start.DayOfWeek == practiceDate.DayOfWeek;

    private static SinglePractice CreateSinglePracticeFromSchedule(
        SchedulePractice schedulePractice, 
        Guid trainerId, 
        DateTime currentStart, 
        DateTime? newStart = null, 
        DateTime? newEnd = null, 
        float? price = null,
        PracticeType? practiceType = null)
    {
        return new SinglePractice(
            schedulePractice.GroupId,
            price ?? schedulePractice.Price,
            newStart ?? SchedulePractice.CombineDateAndTime(currentStart, schedulePractice.Start),
            newEnd ?? SchedulePractice.CombineDateAndTime(currentStart, schedulePractice.End),
            practiceType ?? schedulePractice.PracticeType,
            trainerId,
            schedulePractice.Id,
            SchedulePractice.CombineDateAndTime(currentStart, schedulePractice.Start)
        );
    }

    private async Task<ErrorOr<PracticeDto>> GetPracticeDtoAsync(Guid practiceId)
    {
        return await practiceRepository.GetByIdAsync(practiceId) is SinglePractice practice 
            ? practice.ToDto() 
            : Error.Failure("Practice was not created");
    }
}