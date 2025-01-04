using TrainerJournal.Domain.Common;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.Schedules;

public interface IScheduleRepository : IUnitOfWork
{
    Task<Schedule?> FindByIdAsync(Guid id);
    
    Task<Schedule?> FindGroupActiveScheduleAsync(Guid groupId);
    
    Task<List<Schedule>> SelectByGroupIdAsync(Guid groupId, DateTime start, DateTime end);

    Task<List<Schedule>> SelectByUserIdAsync(Guid userId, DateTime start, DateTime end);
    
    void Add(Schedule schedule);
}