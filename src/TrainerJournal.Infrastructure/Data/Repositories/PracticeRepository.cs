using Microsoft.EntityFrameworkCore;
using TrainerJournal.Application.Services.Practices;
using TrainerJournal.Domain.Entities;
using TrainerJournal.Infrastructure.Common;

namespace TrainerJournal.Infrastructure.Data.Repositories;

public class PracticeRepository(AppDbContext context) : BaseRepository(context), IPracticeRepository
{
    private DbSet<Practice> practices => dbContext.Practices;
    private DbSet<SchedulePractice> schedulePractices => dbContext.SchedulePractices;
    private DbSet<SinglePractice> singlePractices => dbContext.SinglePractices;
    
    public async Task<Practice?> GetByIdAsync(Guid id)
    {
        return await practices
            .Include(p => (p as SinglePractice)!.OverridenPractice)
            .Include(p => p.Group)
            .Include(p => p.Trainer)
                .ThenInclude(t => t.User)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<bool> HasOverridenSinglePracticeAsync(Guid overridenPracticeId, DateTime originalStart)
    {
        return await singlePractices
            .AnyAsync(s => s.OverridenPracticeId == overridenPracticeId && s.OriginalStart == originalStart);
    }

    public async Task<List<SinglePractice>> GetSinglePracticesByUserIdAsync(Guid userId, DateTime start, DateTime end)
    {
        return await singlePractices
            .Include(p => p.Group)
            .ThenInclude(g => g.Students)
            .Where(p => start <= p.Start && p.Start <= end
                                         && (p.TrainerId == userId || p.GroupId == null ||
                                             p.Group.Students.Any(s => s.Id == userId)))
            .ToListAsync();
    }

    public async Task<List<SinglePractice>> GetSinglePracticesByGroupIdAsync(Guid groupId, DateTime start, DateTime end)
    {
        return await singlePractices
            .Include(p => p.Group)
                .ThenInclude(g => g.Students)
            .Where(p => p.GroupId != null && p.GroupId == groupId 
                                          && start <= p.Start && p.Start <= end)
            .ToListAsync();
    }

    public async Task AddRangeAsync(List<SchedulePractice> newPractices)
    {
        await schedulePractices.AddRangeAsync(newPractices);
    }

    public async Task AddAsync(SinglePractice practice)
    {
        await singlePractices.AddAsync(practice);
    }

    public void Remove(SinglePractice practice)
    {
        singlePractices.Remove(practice);
    }
}