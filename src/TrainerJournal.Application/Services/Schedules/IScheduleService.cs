using ErrorOr;
using TrainerJournal.Application.Services.Schedules.Dtos;
using TrainerJournal.Application.Services.Schedules.Dtos.Requests;
using TrainerJournal.Domain.Enums.ViewSchedule;

namespace TrainerJournal.Application.Services.Schedules;

public interface IScheduleService
{
    public Task<ErrorOr<List<ScheduleItemDto>>> GetScheduleAsync(Guid userId, DateTime Date, ViewSchedule View);
    
    public Task<ErrorOr<List<ScheduleItemDto>>> CreateScheduleAsync(Guid trainerId, CreateScheduleRequest request);
}