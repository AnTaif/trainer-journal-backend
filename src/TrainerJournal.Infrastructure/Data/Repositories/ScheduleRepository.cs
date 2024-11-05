using Microsoft.EntityFrameworkCore;
using TrainerJournal.Application.Services.Schedules;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Infrastructure.Data.Repositories;

public class ScheduleRepository(AppDbContext dbContext) : IScheduleRepository
{
    private readonly DbSet<Schedule> schedules = dbContext.Schedules;
    
    public async Task<Schedule?> GetByIdAsync(Guid id)
    {
        return await schedules
            .Include(s => s.Practices)
            .Include(s => s.Group)
                .ThenInclude(g => g.Students)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<List<Schedule>> GetAllByUserIdAsync(Guid userId, DateTime start, DateTime end)
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
            .ToListAsync();
    }

    public async Task<Schedule?> GetGroupActiveScheduleAsync(Guid groupId)
    {
        return await schedules
            .FirstOrDefaultAsync(s => s.GroupId == groupId && s.Until == null);
    }

    public async Task AddAsync(Schedule schedule)
    {
        await schedules.AddAsync(schedule);
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}