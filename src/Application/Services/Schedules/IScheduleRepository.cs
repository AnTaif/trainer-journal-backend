using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Application.Services.Schedules;

public interface IScheduleRepository
{
    public Task<Schedule?> GetByIdAsync(Guid id);
    
    public Task<List<Schedule>> GetAllByGroupIdAsync(Guid groupId, DateTime start, DateTime end);

    public Task<List<Schedule>> GetAllByUserIdAsync(Guid userId, DateTime start, DateTime end);

    public Task<Schedule?> GetGroupActiveScheduleAsync(Guid groupId);
    
    public Task AddAsync(Schedule schedule);
    
    public Task SaveChangesAsync();
}