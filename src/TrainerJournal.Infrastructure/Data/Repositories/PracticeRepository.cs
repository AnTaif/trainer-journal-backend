using Microsoft.EntityFrameworkCore;
using TrainerJournal.Application.Services.Practices;
using TrainerJournal.Domain.Entities;

namespace TrainerJournal.Infrastructure.Data.Repositories;

public class PracticeRepository(AppDbContext dbContext) : IPracticeRepository
{
    private DbSet<Practice> practices => dbContext.Practices;
    
    public async Task<Practice?> GetByIdAsync(Guid id)
    {
        return await practices
            .Include(p => p.Group)
            .Include(p => p.Hall)
            .Include(p => p.Trainer)
                .ThenInclude(t => t.User)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Practice>> GetByUserIdAsync(Guid userId, DateTime startDate, int daysCount)
    {
        return await practices
            .Include(p => p.Group)
                .ThenInclude(g => g.Students)
            .Where(p => (p.TrainerId == userId 
                         || p.Group.Students.Any(s => s.Id == userId))
                        && p.StartDate > startDate 
                        && p.StartDate < startDate + new TimeSpan(daysCount, 0, 0, 0))
            .OrderBy(p => p.StartDate)
            .ToListAsync();
    }

    public async Task<List<Practice>> GetByGroupIdAsync(Guid groupId, DateTime startDate, int daysCount)
    {
        return await practices
            .Include(p => p.Group)
            .Where(p => p.GroupId == groupId 
                        && p.StartDate > startDate 
                        && p.StartDate < startDate + new TimeSpan(daysCount, 0, 0, 0))
            .OrderBy(p => p.StartDate)
            .ToListAsync();
    }
    
    public void Add(Practice practice)
    {
        practices.Add(practice);
    }

    public void AddRange(IEnumerable<Practice> newPractices)
    {
        practices.AddRange(newPractices);
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}