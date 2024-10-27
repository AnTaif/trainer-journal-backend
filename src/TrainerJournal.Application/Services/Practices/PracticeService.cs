using ErrorOr;
using TrainerJournal.Application.Services.Groups;
using TrainerJournal.Application.Services.Practices.Dtos;
using TrainerJournal.Application.Services.Practices.Dtos.Requests;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Domain.Enums.PracticeType;

namespace TrainerJournal.Application.Services.Practices;

public class PracticeService(
    IPracticeRepository practiceRepository,
    IGroupRepository groupRepository) : IPracticeService
{
    public async Task<ErrorOr<PracticeInfoDto>> GetByIdAsync(Guid id, Guid userId)
    {
        var practice = await practiceRepository.GetByIdAsync(id);
        if (practice == null)
            return Error.NotFound(description: "Group not found");
        if (practice.TrainerId != userId && practice.Group.TrainerId != userId)
            return Error.Forbidden(description: "This group is not available to the user");
        
        return practice.ToInfoDto();
    }

    public async Task<ErrorOr<List<PracticeItemDto>>> GetByGroupIdAsync(
        Guid groupId, Guid userId, DateTime startDate, int daysCount)
    {
        var practices = await practiceRepository.GetByGroupIdAsync(groupId, startDate, daysCount);
        
        return practices.Select(p => new PracticeItemDto(p.Id, p.Group.Name, p.StartDate, p.EndDate)).ToList();
    }

    public async Task<ErrorOr<List<PracticeItemDto>>> GetByUserIdAsync(Guid userId, DateTime startDate, int daysCount)
    {
        var practices = await practiceRepository.GetByUserIdAsync(userId, startDate, daysCount);
        
        return practices.Select(p => new PracticeItemDto(p.Id, p.Group.Name, p.StartDate, p.EndDate)).ToList();
    }

    public async Task<ErrorOr<List<PracticeItemDto>>> CreateScheduleAsync(
        CreateScheduleRequest request, Guid groupId, Guid userId)
    {
        var group = await groupRepository.GetByIdAsync(groupId);
        if (group == null)
            return Error.NotFound(description: "Group not found");
        if (group.TrainerId != userId)
            return Error.Forbidden(description: "This group is not available to the user");

        var practices = new List<Practice>();
        var oneWeek = new TimeSpan(7, 0, 0, 0);
        for (var i = 0; i < request.RepeatWeeks; i++)
        {
            var offsetTimeSpan = oneWeek * i;
            practices.AddRange(request.Practices.Select(p => 
                new Practice(
                    groupId, 
                    request.Price, 
                    p.StartTime.ToUniversalTime() + offsetTimeSpan, 
                    p.EndTime.ToUniversalTime() + offsetTimeSpan, 
                    PracticeType.Training, 
                    userId, 
                    group.HallId)));
        }
        
        practiceRepository.AddRange(practices);
        await practiceRepository.SaveChangesAsync();

        return practices.Select(p => new PracticeItemDto(p.Id, p.Group.Name, p.StartDate, p.EndDate)).ToList();
    }
}