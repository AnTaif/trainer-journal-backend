using TrainerJournal.Application.Services.Schedules.Dtos;
using TrainerJournal.Application.Services.Schedules.Dtos.Requests;
using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Enums.ViewSchedule;

namespace TrainerJournal.Application.Services.Schedules;

public interface IScheduleService
{
    public Task<Result<List<ScheduleItemDto>>> GetScheduleAsync(Guid userId, DateTime Date, ViewSchedule View);

    public Task<Result<List<ScheduleItemDto>>> GetGroupScheduleAsync(Guid groupId, DateTime Date, ViewSchedule View);

    public Task<Result<List<ScheduleItemDto>>> CreateScheduleAsync(Guid trainerId, Guid groupId,
        CreateScheduleRequest request);
}