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
            .Where(p => p.TrainerId == userId || p.Group.Students.Any(s => s.UserId == userId))
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
}