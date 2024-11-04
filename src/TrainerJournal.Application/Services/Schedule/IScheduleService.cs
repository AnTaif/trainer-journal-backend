using TrainerJournal.Application.Services.Schedule.Dtos;
using TrainerJournal.Application.Services.Schedule.Dtos.Requests;

namespace TrainerJournal.Application.Services.Schedule;

public interface IScheduleService
{
    public Task<List<ScheduleItemDto>> GetScheduleAsync(Guid userId, GetScheduleRequest request);
    
    public Task<List<ScheduleItemDto>> CreateScheduleAsync(Guid trainerId, CreateScheduleRequest request);
}