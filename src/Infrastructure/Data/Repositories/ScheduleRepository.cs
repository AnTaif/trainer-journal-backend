using Microsoft.EntityFrameworkCore;
using TrainerJournal.Application.Services.Schedules;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Infrastructure.Common;

namespace TrainerJournal.Infrastructure.Data.Repositories;

public class ScheduleRepository(AppDbContext context) : BaseRepository(context), IScheduleRepository
{
    private DbSet<Schedule> schedules => dbContext.Schedules;
    
    public async Task<Schedule?> FindByIdAsync(Guid id)
    {
        return await schedules
            .Include(s => s.Practices)
            .Include(s => s.Group)
                .ThenInclude(g => g.Students)
            .FirstOrDefaultAsync(s => s.Id == id);
    }
    
    public async Task<Schedule?> FindGroupActiveScheduleAsync(Guid groupId)
    {
        return await schedules
            .FirstOrDefaultAsync(s => s.GroupId == groupId && s.Until == null);
    }

    public async Task<List<Schedule>> SelectByGroupIdAsync(Guid groupId, DateTime start, DateTime end)
    {
        return await schedules
            .Include(s => s.Practices)
            .Include(s => s.Group)
            .ThenInclude(g => g.Students)
            .Where(s => s.GroupId == groupId)
            .Where(s => 
                (start >= s.StartDay && start <= (s.Until ?? DateTime.MaxValue)) 
                || (end >= s.StartDay && end <= (s.Until ?? DateTime.MaxValue)) 
                || (start <= s.StartDay && end >= (s.Until ?? DateTime.MaxValue)))
            .OrderBy(s => s.StartDay)
            .ToListAsync();
    }

    public async Task<List<Schedule>> SelectByUserIdAsync(Guid userId, DateTime start, DateTime end)
    {
        return await schedules
            .Include(s => s.Practices)
            .Include(s => s.Group)
                .ThenInclude(g => g.Students)
            .Where(s => s.Group.TrainerId == userId || s.Group.Students.Any(st => st.Id == userId))
            .Where(s => 
                (start >= s.StartDay && start <= (s.Until ?? DateTime.MaxValue)) 
                || (end >= s.StartDay && end <= (s.Until ?? DateTime.MaxValue)) 
                || (start <= s.StartDay && end >= (s.Until ?? DateTime.MaxValue)))
            .OrderBy(s => s.StartDay)
            .ToListAsync();
    }

    public void Add(Schedule schedule)
    {
        schedules.Add(schedule);
    }
}